using System.Reflection;
using System.Runtime.CompilerServices;
using Kuboczoch.FoodHandbook;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;

// Execute trusted API geometry and input code directly, without game startup, GL,
// a world, assets, Harmony, or ACA. Bypass constructors ONLY for rendering resources.
static T Bare<T>() => (T)RuntimeHelpers.GetUninitializedObject(typeof(T));
static void Set(object o,string field,object value) => o.GetType().GetField(field,BindingFlags.Instance|BindingFlags.NonPublic)!.SetValue(o,value);
static SlideshowItemstackTextComponent Icon(int id, List<int> clicks, EnumFloat mode) {
 var icon=Bare<SlideshowItemstackTextComponent>();
 icon.Float=mode;
 icon.VerticalAlign=EnumVerticalAlign.Bottom;
 typeof(RichTextComponentBase).GetProperty("BoundsPerLine")!.SetValue(icon,new[]{new LineRectangled(0,0,GuiElement.scaled(40),GuiElement.scaled(40))});
 Set(icon,"unscaledSize",40d);
 Set(icon,"slot",new DummySlot(new ItemStack()));
 Set(icon,"onStackClicked",(Action<ItemStack>)(_=>clicks.Add(id)));
 return icon;
}
static ClearFloatTextComponent Clear(float margin) {var c=Bare<ClearFloatTextComponent>();c.Float=EnumFloat.None;c.UnscaledMarginTop=margin;return c;}
static GuiElementRichtext Layout(RichTextComponentBase[] nodes,double width) {
 var rich=Bare<GuiElementRichtext>();rich.Components=nodes;
 rich.Bounds=ElementBounds.FixedSize(width,1000).WithParent(ElementBounds.Empty);
 rich.CalcHeightAndPositions();return rich;
}
static void Check(bool ok,string msg) {if(!ok)throw new Exception(msg);Console.WriteLine("PASS "+msg);}
var clicks=new List<int>();
RuntimeEnv.GUIScale=1;
var legacy=new[]{Icon(0,clicks,EnumFloat.Left),Icon(1,clicks,EnumFloat.Left)};
var bad=Layout(legacy,300);
bad.OnMouseDownOnElement(null,new MouseEvent(20,20,EnumMouseButton.Left,0));
Check(legacy[0].BoundsPerLine[0].X==legacy[1].BoundsPerLine[0].X && clicks.SequenceEqual(new[]{0,1}),"0.3.0 reproducer: actual API Left bounds coincide and one click invokes BOTH callbacks");
int cases=0;
foreach(var scale in new[]{0.75f,1f,1.5f,2f}) foreach(var width in new[]{40d,45d,79d,80d,81d,120d,300d}) foreach(var count in new[]{2,3,12}) {
 RuntimeEnv.GUIScale=scale;clicks.Clear();
 var icons=Enumerable.Range(0,count).Select(i=>Icon(i,clicks,ProcessSectionLayout.OutputFloat)).ToArray();
 var nodes=new List<RichTextComponentBase>();
 nodes.Add(Clear(2)); nodes.AddRange(icons); nodes.Add(Clear(3));
 var next=Icon(count,clicks,ProcessSectionLayout.OutputFloat);
 nodes.Add(Clear(2)); nodes.Add(next); nodes.Add(Clear(3));
 var rich=Layout(nodes.ToArray(),width);
 if(next.BoundsPerLine[0].Y<icons.Max(i=>i.BoundsPerLine[0].Y+i.BoundsPerLine[0].Height)) throw new Exception("Adjacent section overlaps previous output row");
 for(int i=0;i<count;i++) {
  var a=icons[i].BoundsPerLine[0];
  for(int j=i+1;j<count;j++) {var b=icons[j].BoundsPerLine[0];if(a.X<b.X+b.Width && b.X<a.X+a.Width && a.Y<b.Y+b.Height && b.Y<a.Y+a.Height)throw new Exception($"OVERLAP count={count} width={width} scale={scale}: {i} ({a.X},{a.Y}) / {j} ({b.X},{b.Y})");}
  clicks.Clear();rich.OnMouseDownOnElement(null,new MouseEvent((int)(rich.Bounds.absX+a.X+a.Width/2),(int)(rich.Bounds.absY+a.Y+a.Height/2),EnumMouseButton.Left,0));
  if(!clicks.SequenceEqual(new[]{i}))throw new Exception($"Expected ONLY output {i}; got {string.Join(',',clicks)}");
  if(a.X<0 || a.X+a.Width>GuiElement.scaled(width)+0.01)throw new Exception("Icon beyond page width");
 }
 if(width<80 && icons[1].BoundsPerLine[0].Y<=icons[0].BoundsPerLine[0].Y)throw new Exception("Narrow page did not wrap");
 cases++;
}
Console.WriteLine($"PASS {cases} trusted API geometry/input cases: distinct output rectangles, single-output clicks, wrapping, four GUI scales");
Console.WriteLine("No real client/ACA/Harmony startup or item rendering exercised.");
