# Native table feasibility spike — VS 1.22.7

## Decision

Feasible as a guide-only custom `RichTextComponentBase`. The supplied trusted API reports `GameVersion.ShortGameVersion == "1.22.7"`. Inspection of that exact assembly finds **no built-in table richtext component**; its VTML converter has no native `table` / `tr` / `td` implementation. The API does expose the necessary extension point (`Tag2RichTextDelegate`, `VtmlUtil.TagConverters`) and real cell layout (`GuiElementRichtext.CalcHeightAndPositions`, `TotalHeight`). No private layout APIs or guessed signatures are used in production.

`ToolsmithTable` uses three equal columns and native richtext components per cell. All rows share the same column boundaries, each row takes its tallest cell's height, and the containing richtext advances past the entire table. A partially occupied preceding line is consumed before the block begins. Native text remains native text; native link and slideshow instances retain their callbacks, hrefs and attributed stacks. Static composition is translated and clipped per cell; interactive rendering receives corresponding offsets. Mouse events and handled state are translated/propagated; native cursor lookup uses cell-local line bounds. Each cell's native richtext is disposed when the table is disposed.

The native word-based layout can overflow on a word wider than its column. The first actual geometry run demonstrated a native text rectangle width 83 in a content width 51. `FitLongWords` now inserts measured line breaks only in oversized words, splitting on Unicode text elements. Original display text is retained separately and restored/rebroken on each layout, so repeated resizing does not accumulate hard breaks or mutate link targets. Ordinary native word wrapping remains responsible for the rest of the text.

## Red / green evidence

- `fail-first.txt`: tests written before the component existed; command exited 1 with CS0103/CS0246 for missing `ToolsmithTable` (after correcting test-only API signature issues). This is the retained missing-feature failure, not claimed as a pre-existing product regression.
- `results.txt`: final actual native headless execution, exit 0, **1146 assertions passed**.
- `build-results.txt`: production library build, exit 0, **0 warnings / 0 errors**.
- `native-static-table.png`: actual CPU Cairo composition of a two-row, three-column table, using native bold/italic text, long unbroken labels, escaped literal angle brackets, and surrounding text. It is **not a game GUI screenshot**.

Commands from repository root (the installed SDK is not on the default PATH):

```sh
export DOTNET_ROOT=/home/brunette/.hermes/cache/scratch/woodworking-tutorial/dotnet
export PATH="$DOTNET_ROOT:$PATH"
dotnet build client/ToolsmithFamilySlideshows.csproj -c Release
dotnet run --project client/geometry/TableGeometry.csproj -c Release
dotnet run --project client/tests/FamilyTests.csproj -c Release -- attribute_inventory.json client/assets/kuboczochmodpackguide/config/toolsmith-families.json
```

The unchanged family suite also exited 0: all 85 actual snapshot variants materialized, all 85 native page codes matched, and all 85 displayed-stack callbacks routed to the handbook. The geometry suite uses an explicitly synthetic attributed handle fixture to exercise the existing family converter *inside* table cells, not to replace those independent inventory checks.

## Geometry coverage

- Fixed handbook widths 240, 320, 420, 550 at GUI scales 0.75, 1, 1.5, 2: all 16 combinations pass.
- Two rows with three equal-width columns; native long-label wrapping; positive cell geometry; no column/row overlap; aligned boundaries; native component line bounds fit cell content widths.
- Native link `OnMouseDown`/`OnMouseUp`, callback count, propagated handled state, outside click rejection and translated link cursor.
- Existing native `SlideshowItemstackTextComponent` inside a cell: translated click goes through the family handbook callback, preserving its attributed stack rather than using the text link callback.
- A table starting after a partial line; following text remains below the table; existing table relayout narrow → wide → narrow restores height; long unbroken label containment.
- Native bold and italic child components, real static Cairo composition, malformed row rejection, client-only registration, previous converter restoration and later-owner preservation.

## Trust and limitations

Only own code, .NET runtime assemblies, and official game assemblies from `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs` are referenced/executed. The official vanilla `Mods/VSSurvivalMod.dll` supplies the native handbook page-code helper; **no third-party mod DLL is loaded**. API/world/input/language proxies provide environment data only. They do not replace native font measurement, line layout, link components, slideshow components or event handlers. Tests map Cairo's `libcairo-2` name to installed `libcairo.so.2` via a test-only resolver and initialize a minimal language service; production relies on the running client for these.

The supplied game installation has no `Vintagestory`, `Vintagestory.exe` or `Vintagestory.dll` client executable; `DISPLAY` and `WAYLAND_DISPLAY` are unset. **A real GUI was not installed/launched.** Native GPU texture upload, item mesh rendering, animated slideshow changes, tooltip rendering, actual handbook scroll interaction and visual appearance in the game remain in-client smoke-test work. Native link composition needs GUI texture upload, so the CPU image deliberately uses static native styled text; it is not represented as a fully rendered interactive screenshot. Test-only slot injection models the already-displayed slideshow stack for native click handlers; it does not simulate animation.

Supported presentation is ordinary native inline handbook richtext (labels, emphasis, links and family/item icons) in three cells; nested tables are rejected. Arbitrary oversized custom components or floated embedded widgets are not claimed as validated. Very narrow widths below 240 logical units are not covered; at tested narrow widths the table becomes taller and requires normal handbook scrolling. No gameplay registration, collectible changes, recipes, stats or mechanics are changed. Content, root build/package files and the existing harness are outside this change's scope.
