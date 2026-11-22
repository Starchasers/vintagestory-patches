#nullable enable
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using HarmonyLib;
using Kuboczoch.VisualMining;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Client.Tesselation;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.Client.NoObf;
using Vintagestory.GameContent;

/// <summary>
/// CPU-only integration through the installed renderer and the engine's own dispatcher.
/// The JSON fixture supplies the drawtype; ChunkTesselator's constructor supplies the dispatch table.
/// No replacement geometry, lighting, mesh-pool supplier, or tessellation implementation is mocked.
/// Only the outside world/atlas lookup and absent GPU client state are supplied by this fixture.
/// Run KVM_REPRO_DISPATCH_LEGACY=1 to remove ONLY the production BleedingCube prefix.
/// </summary>
public static class DispatchMeshTests
{
    const BindingFlags Fields = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    const string ObserverId = "kvm.tests.dispatch.observers";
    static int lightCalls, sourceCalls, emitCalls, beforeCalls;
    static MeshData? source;
    static readonly List<string> logs = new();

    public static void Run(string game)
    {
        RunFixture(game, "wood/woodtyped/log.json", "log-grown-birch-ud", EnumTool.Axe, new() { ["wood"]="birch" });
        foreach (string coverage in new[]{"none","verysparse"}) {
            RunFixture(game, "soil/peat.json", "peat-"+coverage, EnumTool.Shovel, new(){["grasscoverage"]=coverage});
            foreach (string clay in new[]{"blue","red","fire"})
                RunFixture(game,"soil/clay.json",$"rawclay-{clay}-{coverage}",EnumTool.Shovel,new(){["type"]=clay,["grasscoverage"]=coverage});
        }
        string[] Properties(string file) => JObject.Parse(File.ReadAllText(Path.Combine(game,"assets/survival/worldproperties",file)))["variants"]!.Select(v=>(string)(v["code"]??v["Code"])!).ToArray();
        foreach (string fertility in Properties("abstract/fertility.json"))
            foreach(string coverage in Properties("abstract/grasscoverage.json"))
                RunFixture(game,"soil/soil.json",$"soil-{fertility}-{coverage}",EnumTool.Shovel,new(){["fertility"]=fertility,["grasscoverage"]=coverage});
        foreach (string material in new[]{"sand","gravel"})
            foreach(string rock in Properties("block/rock.json"))
                RunFixture(game,$"stone/{material}.json",$"{material}-{rock}",EnumTool.Shovel,new(){["rock"]=rock});

    }
    static void RunFixture(string game, string asset, string code, EnumTool tool, Dictionary<string,string> variants)
    {
        lightCalls = sourceCalls = emitCalls = beforeCalls = 0;
        source = null;
        logs.Clear();
        var fixture = JObject.Parse(File.ReadAllText(Path.Combine(game, "assets/survival/blocktypes", asset)));
        string Resolve(string value) { foreach(var v in variants)value=value.Replace("{"+v.Key+"}",v.Value);return value; }
        JToken? Select(string name) {
            if(fixture[name+"ByType"] is JObject byType)
                return byType.Properties().First(pair=>Vintagestory.API.Util.WildcardUtil.Match(pair.Name,code)).Value;
            return fixture[name];
        }
        var drawType = Enum.Parse<EnumDrawType>((string?)Select("drawtype") ?? "JSON",true);
        Block block = (string?)fixture["class"] switch { "BlockLog"=>new BlockLog(), "BlockSoil"=>new BlockSoil(), "BlockSoilDeposit"=>new BlockSoilDeposit(), _=>new Block() };
        block.BlockId=42;block.Code=new AssetLocation("game:"+code);block.DrawType=drawType;
        block.BlockMaterial=Enum.Parse<EnumBlockMaterial>((string)fixture["blockmaterial"]!,true);
        block.Resistance=(float?)fixture["resistance"] ?? block.Resistance;
        block.RenderPass=(EnumChunkRenderPass)Enum.Parse(typeof(EnumChunkRenderPass),(string?)Select("renderpass") ?? "Opaque",true);
        block.VertexFlags=new VertexFlags();block.Textures=new Dictionary<string, CompositeTexture>();
        Check(!fixture.Properties().Any(p=>new[]{"collisionbox","collisionboxes","sideopaque","sidesolid"}.Contains(p.Name.ToLowerInvariant())),"Asset gained geometry overrides: "+asset);
        Check(block.SideOpaque.All&&block.SideSolid.All&&block.CollisionBoxes.Length==1&&block.CollisionBoxes[0].X2==1,"Actual constructor defaults changed");
        JObject textures = (JObject)(Select("textures") ?? throw new Exception("Missing textures: "+asset));
        foreach(var pair in textures.Properties())
            block.Textures[pair.Name]=new CompositeTexture(new AssetLocation("game",Resolve((string)pair.Value["base"]!)));
        if(drawType==EnumDrawType.TopSoil)
            block.Textures["specialSecondTexture"].Baked=new BakedCompositeTexture { BakedName=block.Textures["specialSecondTexture"].Base,TextureSubId=2 };
        if(Select("climateColorMap")?.Type!=JTokenType.Null && Select("climateColorMap")!=null)block.ClimateColorMapResolved=new ColorMap{RectIndex=1};
        if(Select("seasonColorMap")?.Type!=JTokenType.Null && Select("seasonColorMap")!=null)block.SeasonColorMapResolved=new ColorMap{RectIndex=2};
        // Loader expands all/horizontals into faces; preserve the actual resolved base asset.
        foreach(var face in BlockFacing.ALLFACES)
            if(!block.Textures.ContainsKey(face.Code))block.Textures[face.Code]=block.Textures.TryGetValue("horizontals",out var horizontal)&&face.IsHorizontal ? horizontal : block.Textures["all"];
        var pos = new BlockPos(0).Set(47, 82, -40);
        var allowedDirty = BlockFacing.ALLFACES.Select(f => pos.AddCopy(f)).Append(pos).Select(Key).ToHashSet();
        int dirty = 0, textureLookups = 0;
        var tex = new TextureAtlasPosition { x1 = .125f, y1 = .25f, x2 = .375f, y2 = .5f, atlasTextureId = 1, atlasNumber = 0 };
        var secondary = new TextureAtlasPosition { x1=.625f,y1=.625f,x2=.875f,y2=.875f,atlasTextureId=1,atlasNumber=0 };
        int secondaryLookups=0;
        object Secondary(AssetLocation name){Check(drawType==EnumDrawType.TopSoil && name.Equals(block.Textures["specialSecondTexture"].Base),"Wrong secondary asset: "+name);secondaryLookups++;return secondary;}
        var texSource = StrictProxy.Create<ITexPositionSource>((m, a) => m.Name switch
        {
            "get_AtlasSize" => new Size2i(1024, 1024),
            "get_Item" => Texture((string)a![0]!),
            _ => throw Unexpected("texture source", m)
        });
        object? Texture(string code)
        {
            Check(block.Textures.ContainsKey(code) || BlockFacing.ALLFACES.Any(f => f.Code == code || "inside-" + f.Code == code), "Unexpected texture key: " + code);
            textureLookups++;
            // The real source returns null for optional missing inner-face overrides.
            return code.StartsWith("inside-") && !block.Textures.ContainsKey(code) ? null : tex;
        }
        var tess = StrictProxy.Create<ITesselatorAPI>((m, a) =>
        {
            if (m.Name == "GetTextureSource" && ReferenceEquals(a![0], block)) return texSource;
            throw Unexpected("tesselator API", m);
        });
        var accessor = StrictProxy.Create<IBlockAccessor>((m, a) =>
        {
            if (m.Name == "GetBlock") { Check(Key((BlockPos)a![0]!) == Key(pos), "World read outside target"); return block; }
            if (m.Name == "GetBlockEntity") { Check(allowedDirty.Contains(Key((BlockPos)a![0]!)), "BE read outside target/neighbors"); return null; }
            if (m.Name == "MarkBlockDirty") { Check(allowedDirty.Contains(Key((BlockPos)a![0]!)), "Dirty outside target/neighbors"); dirty++; return null; }
            throw Unexpected("world accessor (gameplay writes forbidden)", m);
        });
        var world = StrictProxy.Create<IClientWorldAccessor>((m, a) => m.Name switch
        {
            "get_BlockAccessor" => accessor,
            "GetBlock" when a![0] is int id && id == block.BlockId => block,
            _ => throw Unexpected("world", m)
        });
        var atlas = StrictProxy.Create<IBlockTextureAtlasAPI>((m, a) => m.Name switch
        {
            "get_SubPixelPaddingX" or "get_SubPixelPaddingY" => 0f,
            "get_UnknownTexturePosition" => tex,
            "get_Item" when a![0] is AssetLocation name => Secondary(name),
            _ => throw Unexpected("atlas", m)
        });
        // Logging is not gameplay: allow default values for logger getters and record every message.
        var logger = StrictProxy.Create<ILogger>((m, a) =>
        {
            logs.Add(m.Name + ": " + string.Join(" ", a ?? Array.Empty<object>()));
            return m.ReturnType == typeof(void) ? null : m.ReturnType.IsValueType ? Activator.CreateInstance(m.ReturnType) : null;
        });
        var api = StrictProxy.Create<ICoreClientAPI>((m, a) => m.Name switch
        {
            "get_World" => world, "get_Tesselator" => tess, "get_BlockTextureAtlas" => atlas, "get_Logger" => logger,
            _ => throw Unexpected("client API", m)
        });

        // A real constructor builds the dispatch table and JsonTesselator. No ClientMain constructor:
        // that would open SDL/OpenGL. Only fields dereferenced by the CPU path are materialized.
        var client = (ClientMain)RuntimeHelpers.GetUninitializedObject(typeof(ClientMain));
        var atlasManager = (BlockTextureAtlasManager)RuntimeHelpers.GetUninitializedObject(typeof(BlockTextureAtlasManager));
        Set(atlasManager, "TextureAtlasPositionsByTextureSubId", new[] { tex, tex, secondary });
        Set(client, "BlockAtlasManager", atlasManager);
        var clientApi = EmptyFieldObject(client, "api");
        EmptyFieldObject(clientApi, "renderapi"); // useSSBOs=false; CPU arrays, no GPU dependency
        var dispatcher = new ChunkTesselator(client);
        var vars = (TCTCache)Get(dispatcher, "vars");
        vars.posY = pos.Y; vars.posZ = pos.Z; vars.dimension = pos.dimension;
        vars.ly = 18; vars.lz = 24; vars.extIndex3d = 19 * 1156 + 25 * 34 + 16;
        vars.aoAndSmoothShadows = false;
        vars.textureAtlasPositionsByTextureSubId = new[]{tex,tex,secondary};
        if(drawType==EnumDrawType.JSON) {
            var source=StrictProxy.Create<ITexPositionSource>((m,a)=>m.Name switch {"get_Item"=>tex,"get_AtlasSize"=>new Size2i(1024,1024),_=>throw Unexpected("shape textures",m)});
            var shape=Newtonsoft.Json.JsonConvert.DeserializeObject<Shape>(File.ReadAllText(Path.Combine(game,"assets/game/shapes/block/basic/cube.json")));
            var tesselator=new ShapeTesselator(client,null!,null!,null!);
            tesselator.TesselateShape(code,shape,out var model,source);
            var shapes=(ShapeTesselatorManager)RuntimeHelpers.GetUninitializedObject(typeof(ShapeTesselatorManager));
            var models=new MeshData[64];models[42]=model;Set(shapes,"blockModelDatas",models);
            var alternates=new MeshData[64][];
            Set(shapes,"altblockModelDatasLod0",alternates);Set(shapes,"altblockModelDatasLod1",alternates);Set(shapes,"altblockModelDatasLod2",alternates);
            vars.shapes=shapes;
        }
        var air = new Block { BlockId = 0, SideOpaque = new SmallBoolArray(0) };
        Array.Fill((Block[])Get(dispatcher, "currentChunkBlocksExt"), air);
        Array.Fill((Block[])Get(dispatcher, "currentChunkFluidBlocksExt"), air);
        Array.Fill((int[])Get(dispatcher, "currentChunkRgbsExt"), unchecked((int)0xffffffff));
        ((Block[])Get(dispatcher, "currentChunkBlocksExt"))[vars.extIndex3d] = block;
        var fastTextures = new int[block.BlockId + 1][];
        fastTextures[block.BlockId] = Enumerable.Repeat(1, 7).ToArray();
        fastTextures[block.BlockId][6]=2;
        Set(dispatcher, "fastBlockTextureSubidsByBlockAndFace", fastTextures);
        Set(dispatcher, "TextureIdToReturnNum", new[] { 1 });
        Set(dispatcher, "quantityAtlasses", 1);
        int[] oldMoves = (int[])TileSideEnum.MoveIndex.Clone();
        new[] { -34, 1, 34, -1, 1156, -1156 }.CopyTo(TileSideEnum.MoveIndex, 0);
        var selected = ((IBlockTesselator[])Get(dispatcher, "blockTesselators"))[(int)block.DrawType];
        Check(selected.GetType() == (drawType switch {EnumDrawType.TopSoil=>typeof(TopsoilTesselator), EnumDrawType.JSON=>typeof(JsonTesselator), _=>typeof(BleedingCubeTesselator)}), "Unexpected actual engine dispatcher: "+selected.GetType().Name);
        var dispatch = typeof(ChunkTesselator).GetMethod("TesselateBlock", Fields, null,
            new[] { typeof(Block), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }, null)!;
        Check(dispatch != null, "Engine block dispatcher signature changed");
        var cubeMethod = AccessTools.Method(typeof(BleedingCubeTesselator), "Tesselate");
        var observers = new Harmony(ObserverId);
        var removed = new List<Patch>();
        bool initialized = false;
        try
        {
            CosmeticVoxelRenderer.Initialize(api);
            initialized = true;
            foreach (var type in new[] { typeof(CubeTesselator), typeof(BleedingCubeTesselator), typeof(JsonTesselator), typeof(BleedingJsonTesselator),typeof(TopsoilTesselator) })
            {
                var info = Harmony.GetPatchInfo(AccessTools.Method(type, "Tesselate"));
                Check(info == null || info.Prefixes.Concat(info.Postfixes).Concat(info.Transpilers).Concat(info.Finalizers)
                    .All(p => IsRendererPatch(p.PatchMethod)), "Other mod patches are not allowed in this integration fixture: " + type.Name);
            }
            Console.WriteLine($"dispatch fixture: {block.Code}, {asset} drawtype={drawType} → {(int)block.DrawType} → {selected.GetType().Name}; other mod patches absent");
            if (Environment.GetEnvironmentVariable("KVM_REPRO_DISPATCH_LEGACY") == "1")
            {
                var patches = Harmony.GetPatchInfo(cubeMethod);
                if (patches != null)
                    foreach (var p in patches.Prefixes.Where(p => p.PatchMethod.DeclaringType?.Name == "BleedingCubeMeshPatch"))
                    { removed.Add(p); observers.Unpatch(cubeMethod, p.PatchMethod); }
                Console.WriteLine($"LEGACY: removed {removed.Count} BleedingCube replacement prefix(es), all other production patches retained");
            }
            // Observers never suppress an original method or modify its result/arguments.
            Observe(observers, AccessTools.Method(typeof(CosmeticVoxelRenderer), "BeforeTesselate"), nameof(BeforeObserved));
            Observe(observers, AccessTools.Method(typeof(JsonTesselator), "SetUpLightRGBs"), nameof(LightObserved));
            Observe(observers, AccessTools.Method(typeof(BlockEntityMicroBlock), "CreateMesh", new[] { typeof(ICoreClientAPI), typeof(List<uint>), typeof(int[]), typeof(int[]), typeof(BlockPos), typeof(uint[]), typeof(int) }), nameof(SourceObserved));
            Observe(observers, typeof(JsonTesselator).GetMethods().Single(m => m.Name == "AddJsonModelDataToMesh" && m.GetParameters().Length == 7), nameof(EmitObserved));
            MeshData Render()
            {
                // Use engine pool initialization and real GetPoolForPass/GetMeshPoolForPass.
                var pools = new MeshData[4][][];
                int passes = Enum.GetValues<EnumChunkRenderPass>().Max(p => (int)p) + 1;
                for (int lod = 0; lod < pools.Length; lod++)
                {
                    pools[lod] = new MeshData[passes][];
                    foreach (var pass in Enum.GetValues<EnumChunkRenderPass>())
                    {
                        var pool = pools[lod][(int)pass] = new MeshData[1];
                        AccessTools.Method(typeof(ChunkTesselator), "InitialiseRenderPassPools").Invoke(dispatcher, new object[] { pool, pass, 16384 });
                    }
                }
                Set(dispatcher, "currentModeldataByRenderPassByLodLevel", pools);
                dispatch!.Invoke(dispatcher, new object[] { block, 15, 63, pos.X, pos.Z, (int)block.DrawType });
                var emitted = pools.SelectMany(l => l).Where(p => p != null).SelectMany(p => p).Where(m => m.VerticesCount > 0).ToArray();
                Check(emitted.Length == 1, "Expected one nonempty actual engine mesh pool, got " + emitted.Length);
                Check(emitted[0].IndicesCount > 0, "Actual pool has no triangles");
                if(drawType==EnumDrawType.TopSoil)Check(ReferenceEquals(emitted[0],pools[1][(int)EnumChunkRenderPass.TopSoil][0]),"Topsoil mesh went to wrong destination renderpass/LOD");
                return emitted[0];
            }
            var vanilla = Render();
            Check(vanilla.VerticesCount == 24 && vanilla.IndicesCount == 36, "Vanilla dispatcher did not emit a six-face cube");
            var damage = new BlockDamage { Position = pos, Block = block, RemainingResistance = block.Resistance * .65f };
            var selection = new BlockSelection { Position = pos, Face = BlockFacing.NORTH, HitPosition = new Vec3d(.5, .5, 0) };
            CosmeticVoxelRenderer.Update(damage, selection, tool);
            var observed = Render();
            Check(Signature(observed) == Signature(vanilla) && dirty == 0, "Observation alone changed emitted geometry/redrew");
            int lightBefore = lightCalls, sourceBefore = sourceCalls, emitBefore = emitCalls, hookBefore = beforeCalls;
            var contact = new ContactTracker();
            contact.Sample(code, 0, 0, tool==EnumTool.Shovel ? 15 : 6);
            Check(contact.Sample(code, 0, tool==EnumTool.Shovel ? 16 : 7, tool==EnumTool.Shovel ? 15 : 6), "Contact crossing missing");
            CosmeticVoxelRenderer.CommitImpact(damage, selection, tool);
            Check(dirty == 7, "Impact must invalidate target and six world-coordinate neighbors exactly once");
            var impacted = Render();
            Console.WriteLine($"CPU pool evidence: vanilla={vanilla.VerticesCount}v/{vanilla.IndicesCount}i {Signature(vanilla)}; committed={impacted.VerticesCount}v/{impacted.IndicesCount}i {Signature(impacted)}; hook/light/source/emission deltas={beforeCalls-hookBefore}/{lightCalls-lightBefore}/{sourceCalls-sourceBefore}/{emitCalls-emitBefore}");
            // Deliberately geometry-first: legacy mode fails on the real unchanged pool, not a patch-presence assertion.
            Check(Signature(impacted) != Signature(vanilla), $"RED: real dispatcher still emitted unchanged vanilla cube after committed contact ({selected.GetType().Name} replacement missing)");
            Check(beforeCalls > hookBefore && lightCalls > lightBefore && sourceCalls > sourceBefore && emitCalls > emitBefore,
                "Missing real BeforeTesselate → SetUpLightRGBs → BlockEntityMicroBlock.CreateMesh → AddJsonModelDataToMesh execution");
            Check(source != null && source.VerticesCount > 0 && textureLookups > 0, "No real microblock source/atlas lookups");
            Check(HasInteriorVertex(impacted, 15, 18, 24), "Changed pool contains no carved interior vertices");
            if(drawType==EnumDrawType.TopSoil) {
                Check(secondaryLookups>0,"Real microblock mesher did not resolve the secondary grass asset");
                Check(impacted.CustomShorts!=null && impacted.CustomShorts.Count==impacted.VerticesCount*2,"Topsoil packed grass UV channel missing/truncated");
                Check(source!.RenderPassesAndExtraBits.Take(source.XyzFacesCount).All(pass=>pass==(short)EnumChunkRenderPass.TopSoil),"Lost TopSoil renderpass");
                Check(impacted.CustomShorts!.Values.Take(impacted.CustomShorts.Count).All(value=>(ushort)value>=16384),"Secondary UVs incorrectly used base atlas rectangle");
                Check(impacted.CustomInts!=null && impacted.CustomInts.Count==impacted.VerticesCount,"Missing color-map data");
                if(block.ClimateColorMapResolved!=null)Check(impacted.CustomInts!.Values.Take(impacted.CustomInts.Count).Any(value=>value!=0),"Grass color maps lost");
                Console.WriteLine($"PASS topsoil secondary asset={block.Textures["specialSecondTexture"].Base}, packed UV2={impacted.CustomShorts.Count}, color data={impacted.CustomInts!.Count}, renderpass=TopSoil");
            }
            CosmeticVoxelRenderer.CommitImpact(damage, selection, tool);
            Check(dirty == 7, "Duplicate unchanged contact redrew twice");
            CosmeticVoxelRenderer.Reset(pos);
            var restored = Render();
            Check(dirty == 14 && Signature(restored) == Signature(vanilla), "Reset did not restore exact vanilla emitted CPU mesh");
            Check(damage.RemainingResistance == block.Resistance * .65f, "Cosmetic path altered vanilla damage");

            for (int i = 0; i < 24; i++) CosmeticVoxelRenderer.Update(damage, selection, EnumTool.Knife);
            foreach (string stage in new[] { "mining-observation", "eligibility-rejected", "progress", "impact-commit", "mesh-replaced", "restore" })
            {
                int messages = logs.Count(line => line.Contains("] " + stage + " #"));
                Check(messages > 0 && messages <= 8, "Missing/unbounded production pipeline diagnostics: " + stage);
            }
            Check(logs.Any(line => line.Contains("unsupported tool")), "Eligibility diagnostic omitted rejection reason");
            Console.WriteLine("PASS production diagnostics: all six pipeline stages emitted; 24 repeated rejections bounded to eight, with reason");
            Console.WriteLine($"PASS dispatcher CPU mesh: committed carve has interior vertices; reset={restored.VerticesCount}v/{restored.IndicesCount}i {Signature(restored)} exactly equals baseline; strict world rejects gameplay writes");
        }
        finally
        {
            observers.UnpatchAll(ObserverId);
            foreach (var p in removed) new Harmony(p.owner).Patch(cubeMethod, prefix: new HarmonyMethod(p.PatchMethod) { priority = p.priority, before = p.before, after = p.after });
            if (initialized) CosmeticVoxelRenderer.Dispose();
            oldMoves.CopyTo(TileSideEnum.MoveIndex, 0);
        }
    }

    static bool IsRendererPatch(MethodInfo method) => method.DeclaringType == typeof(CosmeticVoxelRenderer) || method.DeclaringType?.DeclaringType == typeof(CosmeticVoxelRenderer);
    static void BeforeObserved() => beforeCalls++;
    static void LightObserved() => lightCalls++;
    static void SourceObserved(MeshData __result) { sourceCalls++; source = __result; }
    static void EmitObserved() => emitCalls++;
    static void Observe(Harmony h, MethodBase original, string name) => h.Patch(original, postfix: new HarmonyMethod(typeof(DispatchMeshTests), name));
    static bool HasInteriorVertex(MeshData m, int x, int y, int z) => Enumerable.Range(0, m.VerticesCount).Any(i =>
        Interior(m.xyz[i * 3] - x) || Interior(m.xyz[i * 3 + 1] - y) || Interior(m.xyz[i * 3 + 2] - z));
    static bool Interior(float f) => f > .001f && f < .999f;
    static string Signature(MeshData mesh)
    {
        using var stream = new MemoryStream();
        using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, true))
        {
            writer.Write(mesh.VerticesCount); writer.Write(mesh.IndicesCount);
            foreach (var f in mesh.xyz.Take(mesh.VerticesCount * 3)) writer.Write(f);
            foreach (var f in mesh.Uv.Take(mesh.VerticesCount * 2)) writer.Write(f);
            foreach (var b in mesh.Rgba.Take(mesh.VerticesCount * 4)) writer.Write(b);
            foreach (var f in mesh.Flags.Take(mesh.VerticesCount)) writer.Write(f);
            foreach (var i in mesh.Indices.Take(mesh.IndicesCount)) writer.Write(i);
            writer.Write(mesh.CustomShorts?.Count??0);
            if(mesh.CustomShorts!=null)foreach(var value in mesh.CustomShorts.Values.Take(mesh.CustomShorts.Count))writer.Write(value);
            writer.Write(mesh.CustomInts?.Count??0);
            if(mesh.CustomInts!=null)foreach(var value in mesh.CustomInts.Values.Take(mesh.CustomInts.Count))writer.Write(value);
        }
        return Convert.ToHexString(SHA256.HashData(stream.ToArray()))[..16];
    }
    static (int, int, int, int) Key(BlockPos p) => (p.X, p.Y, p.Z, p.dimension);
    static Exception Unexpected(string boundary, MethodInfo m) => new Exception("Unexpected " + boundary + ": " + m);
    static void Check(bool ok, string message) { if (!ok) throw new Exception(message); }
    static object Get(object value, string name) => AccessTools.Field(value.GetType(), name).GetValue(value)!;
    static void Set(object value, string name, object fieldValue) => AccessTools.Field(value.GetType(), name).SetValue(value, fieldValue);
    static object EmptyFieldObject(object value, string name)
    {
        var field = AccessTools.Field(value.GetType(), name);
        var result = RuntimeHelpers.GetUninitializedObject(field.FieldType);
        field.SetValue(value, result);
        return result;
    }
}
