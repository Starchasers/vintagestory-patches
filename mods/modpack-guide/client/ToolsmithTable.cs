using System.Text;
using Cairo;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;

namespace KuboczochModpackGuide.Client;

/// <summary>Guide-only three-column block. Every cell uses the game's own richtext layout,
/// composition and interactive components; this class only owns the enclosing grid.</summary>
public sealed class ToolsmithTable : RichTextComponentBase
{
    public const string TagName = "toolsmithtable";
    public sealed class Cell
    {
        internal readonly GuiElementRichtext Layout;
        private readonly Dictionary<RichTextComponent, string> originalText;
        internal Cell(ICoreClientAPI api, RichTextComponentBase[] components)
        {
            Layout = new GuiElementRichtext(api, components, ElementBounds.Fixed(0, 0, 1, 1));
            originalText = components.OfType<RichTextComponent>().ToDictionary(c => c, c => c.DisplayText);
        }
        internal void FitLongWords()
        {
            foreach (var pair in originalText)
            {
                var component = pair.Key;
                component.DisplayText = System.Text.RegularExpressions.Regex.Replace(pair.Value, @"\S+", match =>
                {
                    if (component.Font.GetTextExtents(match.Value).XAdvance <= ContentWidth - 2) return match.Value;
                    var split = new StringBuilder();
                    string line = "";
                    var graphemes = System.Globalization.StringInfo.GetTextElementEnumerator(match.Value);
                    while (graphemes.MoveNext())
                    {
                        string ch = graphemes.GetTextElement();
                        if (line.Length > 0 && component.Font.GetTextExtents(line + ch).XAdvance > ContentWidth - 2)
                        { split.Append(line).Append('\n'); line = ""; }
                        line += ch;
                    }
                    return split.Append(line).ToString();
                });
            }
        }
        public RichTextComponentBase[] Components => Layout.Components;
        public double X { get; internal set; }
        public double Y { get; internal set; }
        public double Width { get; internal set; }
        public double Height { get; internal set; }
        public double ContentX { get; internal set; }
        public double ContentY { get; internal set; }
        public double ContentWidth { get; internal set; }
        internal bool Contains(double x, double y) => x >= X && x < X + Width && y >= Y && y < Y + Height;
    }
    private readonly List<Cell> cells;
    public IReadOnlyList<Cell> Cells => cells;

    private ToolsmithTable(ICoreClientAPI api, List<Cell> cells) : base(api)
    {
        this.cells = cells;
        Float = EnumFloat.None;
        BoundsPerLine = [new LineRectangled(0, 0, 0, 0)];
    }

    public static RichTextComponentBase Convert(ICoreClientAPI api, VtmlTagToken tag,
        Stack<CairoFont> fonts, Action<LinkTextComponent> didClickLink)
    {
        var cells = new List<Cell>();
        foreach (var row in TagsOnly(tag, "toolsmithrow"))
        {
            var columns = TagsOnly(row, "toolsmithcell").ToArray();
            if (columns.Length != 3) throw new FormatException("A toolsmithrow requires exactly three toolsmithcell elements");
            foreach (var column in columns)
            {
                // VtmlUtil exposes only a string entry point in 1.22.7. Re-encode its parsed
                // children rather than flattening ContentText, retaining native tags/callbacks.
                string markup = string.Concat(column.ChildElements.Select(Encode));
                var components = VtmlUtil.Richtextify(api, markup, fonts.Peek().Clone(), didClickLink);
                cells.Add(new Cell(api, components));
            }
        }
        if (cells.Count == 0) throw new FormatException("A toolsmithtable requires at least one row");
        return new ToolsmithTable(api, cells);
    }

    private static IEnumerable<VtmlTagToken> TagsOnly(VtmlTagToken parent, string expected)
    {
        foreach (var child in parent.ChildElements)
        {
            if (child is VtmlTextToken text && string.IsNullOrWhiteSpace(text.Text)) continue;
            if (child is not VtmlTagToken tag || tag.Name != expected)
                throw new FormatException($"{parent.Name} only accepts {expected} children");
            yield return tag;
        }
    }

    private static string Encode(VtmlToken token)
    {
        if (token is VtmlTextToken text) return text.Text.Replace("<", "&lt;").Replace(">", "&gt;");
        var tag = (VtmlTagToken)token;
        // Disallow recursive grids: richtext children may be links, emphasis, family icons,
        // and all other native converters, but not another custom table.
        if (tag.Name == TagName) throw new FormatException("Nested toolsmith tables are not supported");
        var result = new StringBuilder("<").Append(tag.Name);
        foreach (var pair in tag.Attributes)
        {
            char quote = pair.Value.Contains('"') ? '\'' : '"';
            if (pair.Value.Contains(quote)) throw new FormatException("VTML attribute contains both quote characters");
            result.Append(' ').Append(pair.Key).Append('=').Append(quote).Append(pair.Value).Append(quote);
        }
        result.Append('>');
        foreach (var child in tag.ChildElements) result.Append(Encode(child));
        return result.Append("</").Append(tag.Name).Append('>').ToString();
    }

    public override EnumCalcBoundsResult CalcBounds(TextFlowPath[] flowPath, double currentLineHeight,
        double offsetX, double lineY, out double nextOffsetX)
    {
        var path = GetCurrentFlowPathSection(flowPath, lineY);
        double startX = path.X1;
        double width = path.X2 - path.X1;
        if (width <= 0) throw new InvalidOperationException("Toolsmith table requires a positive flow width");
        double padding = Math.Min(GuiElement.scaled(6), width / 18);
        double startY = lineY + currentLineHeight;
        double y = startY;
        for (int row = 0; row < cells.Count; row += 3)
        {
            double height = 0;
            for (int col = 0; col < 3; col++)
            {
                var cell = cells[row + col];
                cell.X = startX + width * col / 3;
                cell.Y = y;
                cell.Width = width / 3;
                cell.ContentX = cell.X + padding;
                cell.ContentY = y + padding;
                cell.ContentWidth = cell.Width - 2 * padding;
                cell.Layout.Bounds.fixedWidth = cell.ContentWidth / RuntimeEnv.GUIScale;
                cell.FitLongWords();
                cell.Layout.CalcHeightAndPositions();
                height = Math.Max(height, cell.Layout.TotalHeight + 2 * padding);
            }
            for (int col = 0; col < 3; col++) cells[row + col].Height = height;
            y += height;
        }
        // None-float advancement is max(currentLineHeight, Bounds.Height), not Bounds.Bottom.
        // Include the preceding partial line in the block's height so following text never overlaps.
        BoundsPerLine = [new LineRectangled(startX, lineY, width, y - lineY)];
        nextOffsetX = 0;
        return EnumCalcBoundsResult.Nextline;
    }

    public override void ComposeElements(Context ctx, ImageSurface surface)
    {
        foreach (var cell in cells)
        {
            ctx.Save();
            try
            {
                ctx.Rectangle(cell.X, cell.Y, cell.Width, cell.Height);
                ctx.Clip();
                ctx.SetSourceRGBA(0.45, 0.35, 0.22, 0.35);
                ctx.LineWidth = GuiElement.scaled(1);
                ctx.Rectangle(cell.X, cell.Y, cell.Width, cell.Height);
                ctx.Stroke();
                ctx.Translate(cell.ContentX, cell.ContentY);
                foreach (var component in cell.Components) component.ComposeElements(ctx, surface);
            }
            finally { ctx.Restore(); }
        }
    }

    public override void RenderInteractiveElements(float deltaTime, double renderX, double renderY, double renderZ)
    {
        foreach (var cell in cells)
            foreach (var component in cell.Components)
                component.RenderInteractiveElements(deltaTime, renderX + cell.ContentX, renderY + cell.ContentY, renderZ);
    }

    private void Dispatch(MouseEvent args, Action<RichTextComponentBase, MouseEvent> action)
    {
        foreach (var cell in cells)
        {
            // Forward out-of-cell coordinates too: native link components must reset their
            // pressed/hover state on the next event. They perform their own line-rectangle hit test.
            var local = new MouseEvent((int)Math.Floor(args.X - cell.ContentX), (int)Math.Floor(args.Y - cell.ContentY),
                args.DeltaX, args.DeltaY, args.Button, args.Modifiers) { Handled = args.Handled };
            foreach (var component in cell.Components) action(component, local);
            args.Handled |= local.Handled;
        }
    }
    public override void OnMouseMove(MouseEvent args) => Dispatch(args, (c, e) => c.OnMouseMove(e));
    public override void OnMouseDown(MouseEvent args) => Dispatch(args, (c, e) => c.OnMouseDown(e));
    public override void OnMouseUp(MouseEvent args) => Dispatch(args, (c, e) => c.OnMouseUp(e));

    public override bool UseMouseOverCursor(ElementBounds richtextBounds)
    {
        double x = api.Input.MouseX - richtextBounds.absX, y = api.Input.MouseY - richtextBounds.absY;
        MouseOverCursor = null!;
        foreach (var cell in cells)
        {
            if (!cell.Contains(x, y)) continue;
            foreach (var component in cell.Components)
                if (component.MouseOverCursor != null && component.BoundsPerLine.Any(b => b.PointInside(x - cell.ContentX, y - cell.ContentY)))
                { MouseOverCursor = component.MouseOverCursor; return true; }
        }
        return false;
    }
    public override void Dispose()
    {
        foreach (var cell in cells) cell.Layout.Dispose();
        base.Dispose();
    }
}

/// <summary>Separate client-only registration; does not change the existing family converter.</summary>
public sealed class ToolsmithTableClientSystem : ModSystem
{
    private Tag2RichTextDelegate? previous;
    private Tag2RichTextDelegate? registered;
    public override bool ShouldLoad(EnumAppSide side) => side == EnumAppSide.Client;
    public override void StartClientSide(ICoreClientAPI api)
    {
        if (registered != null) return;
        VtmlUtil.TagConverters.TryGetValue(ToolsmithTable.TagName, out previous);
        registered = ToolsmithTable.Convert;
        VtmlUtil.TagConverters[ToolsmithTable.TagName] = registered;
    }
    public override void Dispose()
    {
        if (registered != null && VtmlUtil.TagConverters.TryGetValue(ToolsmithTable.TagName, out var current) && current == registered)
        {
            if (previous == null) VtmlUtil.TagConverters.Remove(ToolsmithTable.TagName);
            else VtmlUtil.TagConverters[ToolsmithTable.TagName] = previous;
        }
        registered = null;
    }
}
