using System.Reflection;
using System.Runtime.InteropServices;
using KuboczochModpackGuide.Client;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;

public class Proxy : DispatchProxy
{
    public System.Func<MethodInfo,object?[]?,object?>? Handler;
    protected override object? Invoke(MethodInfo? m, object?[]? a) => Handler?.Invoke(m!,a) ?? (m!.ReturnType.IsValueType ? Activator.CreateInstance(m.ReturnType) : null);
    public static T Make<T>(System.Func<MethodInfo,object?[]?,object?> f) where T:class { var o=Create<T,Proxy>(); ((Proxy)(object)o).Handler=f; return o; }
}
public static class Program
{
    static int checks;
    static void Check(bool value,string name) { if(!value) throw new Exception(name); checks++; }
    public static void Main()
    {
        NativeLibrary.SetDllImportResolver(typeof(Cairo.Context).Assembly,(n,a,p)=>n=="libcairo-2"?NativeLibrary.Load("libcairo.so.2"):IntPtr.Zero);
        Check(GameVersion.ShortGameVersion=="1.22.7","trusted exact API");
        Check(!typeof(RichTextComponentBase).Assembly.GetTypes().Any(t=>typeof(RichTextComponentBase).IsAssignableFrom(t)&&t.Name.Contains("Table")),"native API has no built-in table richtext component");
        var native=typeof(RichTextComponentBase).Assembly;
        Console.WriteLine("SPIKE API="+GameVersion.ShortGameVersion+" native table component types="+string.Join(",",native.GetTypes().Where(t=>typeof(RichTextComponentBase).IsAssignableFrom(t)&&t.Name.Contains("Table")).Select(t=>t.Name)));
        var logger=Proxy.Make<ILogger>((m,a)=>null);
        var registry=Proxy.Make<IClassRegistryAPI>((m,a)=>m.ReturnType==typeof(Dictionary<string,Type>)?new Dictionary<string,Type>():null);
        var item = new Item { Code = new AssetLocation("toolsmith:handle"), ItemId = 1 };
        var world = Proxy.Make<IClientWorldAccessor>((m,a)=>m.Name switch { "GetItem"=>item,"get_Logger"=>logger,_=>null });
        string? routed = null;
        var protocols = new Dictionary<string,Action<LinkTextComponent>> { ["handbook"] = link => routed = link.Href };
        int mouseX=0, mouseY=0;
        var input = Proxy.Make<IInputAPI>((m,a)=>m.Name switch { "get_MouseX"=>mouseX,"get_MouseY"=>mouseY,_=>null });
        var api=Proxy.Make<ICoreClientAPI>((m,a)=>m.Name switch { "get_Logger"=>logger,"get_ClassRegistry"=>registry,"get_World"=>world,"get_LinkProtocols"=>protocols,"get_Input"=>input,_=>null });
        var family = new ToolsmithFamilySlideshows("{\"codes\":[{\"code\":\"toolsmith:handle\",\"kind\":\"item\",\"stacks\":[{\"attributes\":{\"wood\":\"oak\"}}]}]}");
        VtmlUtil.TagConverters[ToolsmithFamilySlideshows.TagName] = family.Convert;
        VtmlUtil.TagConverters[ToolsmithTable.TagName]=ToolsmithTable.Convert;
        typeof(Lang).GetProperty("CurrentLocale")!.SetValue(null, "en");
        Lang.AvailableLanguages["en"] = Proxy.Make<ITranslationService>((m,a)=>null);
        var font=CairoFont.WhiteSmallText();
        string row="<toolsmithrow><toolsmithcell><toolsmithfamily code=\"toolsmith:handle\"/><strong>Exceptionally long woodworking tool labels for narrow handbook panels</strong></toolsmithcell><toolsmithcell><a href=\"handbook://test\">A linked comparison with several words that must wrap</a></toolsmithcell><toolsmithcell>Output results across multiple materials and qualities</toolsmithcell></toolsmithrow>";
        foreach(double scale in new[]{0.75,1.0,1.5,2.0}) foreach(double width in new[]{240.0,320.0,420.0,550.0})
        {
            RuntimeEnv.GUIScale=(float)scale;
            int clicked=0;
            var components=VtmlUtil.Richtextify(api,"Before<br/><toolsmithtable>"+row+row+"</toolsmithtable>After",font,_=>clicked++);
            var table=components.OfType<ToolsmithTable>().Single();
            var outer=new GuiElementRichtext(api,components,ElementBounds.Fixed(0,0,width,1));
            outer.CalcHeightAndPositions();
            var cells=table.Cells;
            Check(cells.Count==6,"six richtext cells");
            Check(table.BoundsPerLine[0].Width<=width*scale+0.01,"fits available width");
            Check(cells[0].Height>font.GetFontExtents().Height*2,"long labels wrap");
            for(int i=0;i<cells.Count;i++)
            {
                var cell=cells[i];
                Check(cell.ContentWidth>0&&cell.Height>0,"positive geometry");
                Check(cell.X>=0&&cell.X+cell.Width<=width*scale+0.01,"cell within table");
                if(i>=3) Check(Math.Abs(cell.X-cells[i-3].X)<0.01,"columns aligned across rows");
                foreach(var comp in cell.Components) foreach(var b in comp.BoundsPerLine)
                    Check(b.X>=-0.01&&b.X+b.Width<=cell.ContentWidth+1.01,$"native wrapped component contained: cell={i} x={b.X} w={b.Width} available={cell.ContentWidth} text={(comp as RichTextComponent)?.DisplayText}");
                if(i%3!=2) Check(cell.X+cell.Width<=cells[i+1].X+0.01,"no column overlap");
            }
            Check(cells[0].Y+cells[0].Height<=cells[3].Y+0.01,"no row overlap");
            var after=(RichTextComponent)components.Last();
            Check(after.BoundsPerLine[0].Y>=table.BoundsPerLine[0].Y+table.BoundsPerLine[0].Height-0.01,"following text below table");
            var link=cells[1].Components.OfType<LinkTextComponent>().Single();
            var b0=link.BoundsPerLine[0];
            int x=(int)Math.Ceiling(cells[1].ContentX+b0.X+2),y=(int)Math.Ceiling(cells[1].ContentY+b0.Y+2);
            table.OnMouseDown(new MouseEvent(x,y,EnumMouseButton.Left,0));
            var up=new MouseEvent(x,y,EnumMouseButton.Left,0); table.OnMouseUp(up);
            Check(clicked==1&&up.Handled,"native link callback and handled propagate");
            table.OnMouseDown(new MouseEvent(-2,-2,EnumMouseButton.Left,0)); table.OnMouseUp(new MouseEvent(-2,-2,EnumMouseButton.Left,0));
            Check(clicked==1,"outside click cannot activate cell");
            mouseX=x; mouseY=y;
            Check(table.UseMouseOverCursor(ElementBounds.Empty) && table.MouseOverCursor=="linkselect","native link cursor translated");
            var icon=cells[0].Components.OfType<SlideshowItemstackTextComponent>().Single();
            typeof(SlideshowItemstackTextComponent).GetField("slot",BindingFlags.Instance|BindingFlags.NonPublic)!.SetValue(icon,new DummySlot(icon.Itemstacks[0]));
            var ib=icon.BoundsPerLine[0]; routed=null;
            table.OnMouseDown(new MouseEvent((int)(cells[0].ContentX+ib.X+ib.Width/2),(int)(cells[0].ContentY+ib.Y+ib.Height/2),EnumMouseButton.Left,0));
            Check(routed!=null && routed.StartsWith("handbook://item-toolsmith:handle") && clicked==1,"native family click routes attributed handbook instead of text callback");
            Check(icon.Itemstacks[0].Attributes.GetString("wood")=="oak","attributed family variant retained in cell");
            Console.WriteLine($"GEOMETRY width={width} scale={scale} tableHeight={table.BoundsPerLine[0].Height:F2} rowHeight={cells[0].Height:F2} click=PASS");
        }
        RuntimeEnv.GUIScale=1;
        string staticRow="<toolsmithrow><toolsmithcell><strong>ExtraordinarilyLongUnbrokenToolsmithLabel</strong></toolsmithcell><toolsmithcell><i>Native styled comparison text wraps across column two.</i></toolsmithcell><toolsmithcell>Visible result &lt; example &gt; with full row height</toolsmithcell></toolsmithrow>";
        var staticComponents=VtmlUtil.Richtextify(api,"Preceding text<toolsmithtable>"+staticRow+staticRow+"</toolsmithtable>Following text",font);
        var staticTable=staticComponents.OfType<ToolsmithTable>().Single();
        Check(staticTable.Cells.All(cell=>cell.Components.Length>0),"native styling preserves all cell text");
        Check(staticTable.Cells[1].Components.OfType<RichTextComponent>().Single().Font.Slant==Cairo.FontSlant.Italic,"native italic style retained");
        var staticOuter=new GuiElementRichtext(api,staticComponents,ElementBounds.Fixed(0,0,240,1));
        staticOuter.CalcHeightAndPositions();
        double narrowHeight=staticTable.BoundsPerLine[0].Height;
        Check(staticTable.Cells[0].Y>=staticComponents[0].BoundsPerLine[0].Height,"block starts below preceding partial line");
        staticOuter.Bounds.fixedWidth=550; staticOuter.CalcHeightAndPositions();
        Check(staticTable.BoundsPerLine[0].Height<narrowHeight,"existing table reflows wider");
        staticOuter.Bounds.fixedWidth=240; staticOuter.CalcHeightAndPositions();
        Check(Math.Abs(staticTable.BoundsPerLine[0].Height-narrowHeight)<0.01,"relayout restores original text without cumulative breaks");
        foreach(var cell in staticTable.Cells) foreach(var comp in cell.Components) foreach(var bounds in comp.BoundsPerLine)
            Check(bounds.X+bounds.Width<=cell.ContentWidth+1,"unbroken long labels contained");
        using(var surface=new Cairo.ImageSurface(Cairo.Format.Argb32,240,(int)Math.Ceiling(staticOuter.TotalHeight)+2))
        using(var ctx=new Cairo.Context(surface))
        {
            foreach(var component in staticComponents) component.ComposeElements(ctx,surface);
            surface.WriteToPng("client/geometry/native-static-table.png");
            Check(new FileInfo("client/geometry/native-static-table.png").Length>1000,"native Cairo static composition produced PNG");
        }
        var prior=VtmlUtil.TagConverters[ToolsmithTable.TagName];
        var system=new ToolsmithTableClientSystem();
        Check(system.ShouldLoad(EnumAppSide.Client)&&!system.ShouldLoad(EnumAppSide.Server),"guide converter client only");
        system.StartClientSide(api); system.Dispose();
        Check(VtmlUtil.TagConverters[ToolsmithTable.TagName]==prior,"registration restores prior converter");
        system.StartClientSide(api);
        Tag2RichTextDelegate replacement=(a,t,f,c)=>new RichTextComponent(a,"replacement",f.Peek());
        VtmlUtil.TagConverters[ToolsmithTable.TagName]=replacement; system.Dispose();
        Check(VtmlUtil.TagConverters[ToolsmithTable.TagName]==replacement,"dispose preserves later converter owner");
        VtmlUtil.TagConverters[ToolsmithTable.TagName]=prior;
        bool rejected=false;
        try { VtmlUtil.Richtextify(api,"<toolsmithtable><toolsmithrow><toolsmithcell>bad</toolsmithcell></toolsmithrow></toolsmithtable>",font); } catch(FormatException) { rejected=true; }
        Check(rejected,"reject malformed rows");
        Console.WriteLine($"PASS {checks} assertions; native font metrics/wrapping/layout/link events, no game GUI or untrusted mod DLL loaded");
        RealPages.Run();
        ProcessPages.Run();
    }
}
