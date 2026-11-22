# 0.3.1 layout regression (Kuboczoch)

## Proven cause and fix
Both independent output constructors used `EnumFloat.Left` (numeric 2), not native `Inline` (numeric 1). This is a geometry bug, not output deduplication or recipe matching. The game `SlideshowItemstackTextComponent.CalcBounds` consumes the supplied `offsetX`, creates a 40px square, and reports a next offset. `GuiElementRichtext.CalcHeightAndPositions` (decompiled lines 159–162) only advances the horizontal cursor for `Inline`; `Left` instead reserves a text-flow area without consuming that cursor. Consecutive square left floats therefore receive identical offsets. Its mouse dispatcher forwards the click to every component, and each overlapping slideshow invokes its own callback. The regression actually reproduces two callbacks from one click.

Read-only exact-build evidence: `../food-layout-evidence/EnumFloat.cs`, `GuiElementRichtext.cs`, `ClearFloatTextComponent.cs` (decompiled trusted VintagestoryAPI.dll); `../food-handbook-hook-spike/evidence/slideshow.cs`; native `exact-handbook.cs` lines 629–635 and 695–701 construct output slideshows with `(EnumFloat)1`, which is Inline, not Left. No upstream source is redistributed in the ZIPs.

Shared `ProcessSectionLayout` now owns explicit single-output Inline icon construction and leading/trailing clear row spacers for both ACA and native grid/barrel process paths. All existing provider-first click destinations remain unchanged. Native Mixing objects and callbacks remain retained, not reconstructed. Each ACA section now ends its own row, not just the final section.

## Executed tests
`docs/layout-tdd-red.txt` records a real failure under Left: the actual trusted API produced coincident rectangles and called BOTH callbacks; the first nonoverlap assertion failed.
`tests/Layout` references the trusted game API directly, exercises `CalcHeightAndPositions`, slideshow `CalcBounds`, richtext mouse dispatch and slideshow hit-testing. Constructors with GL/font/world resources are bypassed with uninitialized objects; size, bounds, float mode, dummy slot and callback are initialized explicitly. Clear components also bypass font/localization initialization but execute their original CalcBounds implementation with the production margins. This is not a reimplemented geometry model and not a screenshot/rendering test.
84 cases cover 2/3/12 outputs, widths 40/45/79/80/81/120/300, four GUI scales, adjacent sections, rectangle nonoverlap, actual one-output callback dispatch, in-page bounds and narrow wrapping. A page narrower than one 40px icon is outside the guarantee. Existing 36 semantic tests include exact native Mixing-widget/callback identity preservation. Static checks bind both production call sites and their spacers to the tested shared policy. Logs live alongside this document.

Build and package (no upstream/server edits, no client install):
```
DOTNET=/home/brunette/.hermes/cache/scratch/kuboczoch-visual-mining/research/agent-investigation-engine/dotnet/dotnet python tools/build.py
```

## Runtime acceptance still required
In a real client check blueberry Fermenting yogurt starter portion/vinegar, >=3 Kneading outputs, two bone Simmering outputs and two native Mixing widgets at ordinary/narrow page width and changed GUI scale. Each output must be distinct and open only its own target; Mixing widget-specific behavior must remain intact. No ACA/Harmony startup, live registry, GL render or real client session has been exercised. Section-title link/localization changes are a separate integration pending authoritative destination research.
