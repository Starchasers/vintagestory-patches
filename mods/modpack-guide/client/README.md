# Toolsmith attributed family slideshows

## Integration

Only this `client/` directory was changed. No content, build or existing harness edits are required by the implementation itself.

1. Package `client/bin/Release/net10.0/KuboczochModpackGuide.Client.dll` at the guide mod's archive root alongside its `modinfo.json`. The containing mod must be a code mod (`"type": "code"`); keep the guide's existing mod ID/domain. Do not package game assemblies or the test executable.
2. Copy `client/assets/kuboczochmodpackguide/config/toolsmith-families.json` to archive path `assets/kuboczochmodpackguide/config/toolsmith-families.json`.
3. Replace selected ordinary slideshow markup in the guide with exact inventory codes, e.g. `<toolsmithfamily code="toolsmith:handle"/>` or `<toolsmithfamily code="toolsmith:handle|toolsmith:carpentedhandle"/>`. Use only actual inventory codes; no wildcard expansion. Unknown codes contribute no stacks.
4. The included `ToolsmithFamilyClientSystem` registers in `StartClientSide`, only on the client; no manual registration is necessary. Unloading restores an earlier converter if present.

## Public harness surface

Namespace `KuboczochModpackGuide.Client`, class `ToolsmithFamilySlideshows`:

```csharp
var families = new ToolsmithFamilySlideshows(File.ReadAllText(snapshotPath));
ItemStack[] stacks = families.BuildStacks(clientApi, "toolsmith:handle");
SlideshowItemstackTextComponent? component = families.CreateComponent(clientApi, "toolsmith:handle");
Tag2RichTextDelegate converter = families.Convert;
ToolsmithFamilySlideshows.OpenStackHandbook(clientApi, stacks[0]);
```

Uses `ICoreClientAPI.World.GetItem/GetBlock`, `new ItemStack(collectible)`, and fresh `TreeAttribute`/`StringAttribute` construction. No Toolsmith assembly is referenced or loaded; no creative-stack or collectible-behavior callbacks are invoked. JSON is parsed with `System.Text.Json` and only object/string attribute values are accepted. It does not deserialize class/type metadata.

The component is the actual game `SlideshowItemstackTextComponent`. Its explicit click callback computes `GuiHandbookItemStackPage.PageCodeForStack(clickedStack)` and invokes `api.LinkProtocols["handbook"]` with a `LinkTextComponent` whose href is the full native `handbook://...` address. It does not rely on a surrounding richtext link callback. Missing protocol is a safe no-op. Empty selections return null instead of constructing a native component with an empty stack array.

## Build and headless verification

From repository root:

```sh
/home/brunette/.hermes/cache/scratch/woodworking-tutorial/dotnet/dotnet build client/ToolsmithFamilySlideshows.csproj -c Release
/home/brunette/.hermes/cache/scratch/woodworking-tutorial/dotnet/dotnet run --project client/tests/FamilyTests.csproj -c Release -- attribute_inventory.json client/assets/kuboczochmodpackguide/config/toolsmith-families.json
```

`GameDir` can be overridden with `-p:GameDir=...` for the library build. Current build references the supplied exact game assemblies under `woodworking-tutorial/vs` and verifies API version 1.22.7. No NuGet dependencies are needed. Release library build: zero warnings/errors.

Tests are also callable through public `FamilyTests.Run(inventoryPath, snapshotPath)` in the test assembly. They instantiate native components with API/world proxies, inject native DummySlot values through reflection, and exercise real `OnMouseDown`. All 85 snapshot page codes and all 85 click targets are compared against the independent source inventory. Tests also cover combined families, duplicate suppression, independent attribute trees, missing collectibles/protocol, empty families, installed native delegate signature, native VTML parser, actual converter invocation, and rejection of unsupported attribute values.

On Linux the test registers a test-only Cairo native-library resolver mapping the game's Windows-style `libcairo-2` name to installed `libcairo.so.2`. This permits font extents/component initialization without a GUI launch. The test also initializes a minimal language service, which a running game normally provides. No GUI was launched; visual rendering remains an in-game smoke-test item.

## Native three-column handbook table

The same client DLL now also includes `ToolsmithTableClientSystem`, which independently registers `toolsmithtable` on the client and restores any prior converter on unload. Existing `toolsmithfamily` conversion/callback logic is unchanged. No additional DLL or gameplay system is needed.

```xml
<toolsmithtable>
  <toolsmithrow>
    <toolsmithcell><strong>Family</strong></toolsmithcell>
    <toolsmithcell><strong>Comparison</strong></toolsmithcell>
    <toolsmithcell><strong>Result</strong></toolsmithcell>
  </toolsmithrow>
  <toolsmithrow>
    <toolsmithcell><toolsmithfamily code="toolsmith:handle"/> Long family label</toolsmithcell>
    <toolsmithcell><i>Native styled text</i> and a <a href="handbook://item-toolsmith:handle">handbook link</a></toolsmithcell>
    <toolsmithcell>Wrapping result text</toolsmithcell>
  </toolsmithrow>
</toolsmithtable>
```

Each row requires exactly three `toolsmithcell` elements. Whitespace between structural tags is allowed; non-whitespace content outside cells, empty tables and recursive tables are rejected. Cells retain native VTML tags, not HTML: for example native italics use `<i>`, not `<em>`. The block consumes the available richtext flow width in three equal columns and grows vertically with the tallest cell per row. Overlong words are measured/broken without changing hrefs, and original text is retained for relayout.

Public integration surface: `ToolsmithTable.TagName`, `ToolsmithTable.Convert`, and read-only `ToolsmithTable.Cells` geometry/native component access. A caller's native richtext link callback is forwarded into all cells, while the unchanged family component keeps its explicit displayed-stack handbook routing.

```sh
export DOTNET_ROOT=/home/brunette/.hermes/cache/scratch/woodworking-tutorial/dotnet
export PATH="$DOTNET_ROOT:$PATH"
dotnet run --project client/geometry/TableGeometry.csproj -c Release
```

See [`geometry/FEASIBILITY.md`](geometry/FEASIBILITY.md), retained red/green command logs, and the genuine native Cairo static-composition PNG. Exact API 1.22.7 headless geometry and events pass at widths 240/320/420/550 and GUI scales 0.75/1/1.5/2. A real GUI client is not installed: GPU item rendering, tooltip/animation behavior, texture upload and in-game handbook scrolling still require an in-client smoke test. The PNG proves static CPU composition, not a launched game window. Do not package `client/geometry/`, test binaries, or official game assemblies in the release mod.
