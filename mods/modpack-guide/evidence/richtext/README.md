# VS 1.22.7 richtext item links: exact-version evidence

## Recommended asset-only syntax

```xml
<itemstack type="item" code="example:widget"/> <a href="handbook://item-example:widget">Widget</a>
```

For base-game items the page code omits the `game:` domain:

```xml
<itemstack type="item" code="game:stick"/> <a href="handbook://item-stick">Stick</a>
```

**The icon itself is NOT a native item-page link in stock 1.22.7 VTML.** The adjacent anchor is the working navigation affordance. Do not claim that an asset-only `<itemstack>` automatically opens the item page. `VtmlUtil` constructs `SlideshowItemstackTextComponent` without the optional `onStackClicked` parameter, leaving it null. Its actual `OnMouseDown` only invokes that callback when supplied. `GuiHandbookTextPage.Init` also calls `Richtextify` with a null link callback, so anchors use the registered native `handbook` protocol. An `<a>` containing an `<itemstack>` is not a supported way to make the icon clickable: the anchor branch creates a text link from `ContentText`, not recursively rendered child components.

## Compact slideshow

```xml
<itemstack type="item" code="game:stick|game:flint"/>
```

Use literal `|` with no padding whitespace and a fully qualified code for each alternative. This produces one cycling component, not one icon per code. The converter simply splits on `|`, with no trimming. All alternatives share the one `type` value. A neighboring anchor has a fixed target and does not follow the changing icon. For individually navigable entries, use separate icon + anchor pairs.

## Supported attributes

The built-in `itemstack` branch reads only:

- `type`: exact lowercase `item` selects items; omitted or any other value selects blocks.
- `code`: one asset code or pipe-delimited alternatives. If missing, takes tag `ContentText`.
- `floattype`: parsed as `EnumFloat` (case-sensitive default parser); fallback `Inline`.
- `rsize`: multiplies the default render-size multiplier (1.3); does not set component bounds.
- `offx`, `offy`: GUI-scaled render offsets.

No native `attributes`, `attrs`, `stacksize`, `size`, `href`, `onclick`, `groupBy`, or callback attribute is consumed by this branch. Parser acceptance of arbitrary XML attributes does not imply converter support. The converter creates bare `new ItemStack(collectibleObject)` instances. Invalid lookups fall back to block ID 0; they do not reliably expose missing assets as errors.

The converter's explicit-array slideshow constructor does **not** consult collectible `handbook.groupBy`. A separate programmatic slideshow constructor supports that grouping; do not confuse it with VTML behavior. `GuiHandbookGroupedItemstackPage.PageCode` is its `Name`, but the stock survival handbook page factory enumerates handbook stacks into individual `GuiHandbookItemStackPage` instances, not grouped pages.

## Native page codes and routing

`GuiHandbookItemStackPage.PageCodeForStack` is authoritative:

- `game:stick` item, no significant attributes: `item-stick`.
- `example:widget` item: `item-example:widget`.
- `game:stone-granite` block: `block-stone-granite`.
- Nonignored stack attributes append `-` and the native sorted JSON attribute representation. Ignored attributes and `durability` are removed first. Avoid manually inventing attribute-bearing destinations; VTML itemstack cannot specify those attributes anyway.

`LinkTextComponent.Trigger()` calls an explicit link callback if supplied; otherwise `HandleLink()` dispatches its registered protocol. `ModSystemSurvivalHandbook` registers `handbook`; its handler removes `handbook://`, strips backslashes, and calls `dialog.OpenDetailPageFor(text)` (except `tab-` destinations). `GuiDialogHandbook.OpenDetailPageFor` performs an exact `pageNumberByPageCode` lookup. Anchor navigation is not a fuzzy item search and does not perform the fallback retry found in the separate held-item hotkey code. A syntactically valid destination must still correspond to a registered native handbook page in the actual modpack.

## Evidence and execution scope

- Decompiled exact base DLL methods are retained in this directory as `.cs`; `GameVersion.cs` declares `1.22.7`.
- `base-sha256.json` fingerprints current trusted API, core library, and official survival module. No downloaded modpack DLL was loaded or executed.
- `static-audit.json` records executed source checks for the converter's accepted attributes, null callback, pipe split, explicit-array grouping, and protocol routing.
- `harness-output.txt`: **13 passing checks**, executed against official base assemblies. Covers actual parser, native page-code generation (base/custom domains, block prefix, ignored durability and meaningful attributes), actual slideshow `OnMouseDown` callback/bounds/null behavior, and actual link `Trigger` protocol/callback behavior.
- The harness allocates uninitialized richtext components to isolate event methods from GUI constructors. It supplies explicit stub slots, bounds, callbacks and a DispatchProxy protocol registry. Synthetic test collectibles are plain trusted base `Item`/`Block` objects, not claims about modpack registry contents.
- No game client, renderer, world bootstrap, asset loader, modloader, network, or downloaded mod assemblies execute in the harness. Full `VtmlUtil.Richtextify` icon construction, tooltip rendering, actual page GUI opening, and modpack page existence remain **static-only / client-unverified**. The stock native handbook handler's route to exact page lookup is decompiled evidence, not a rendered integration test.

Re-run from the workdir (all writes stay there):

```sh
export DOTNET_ROOT=/home/brunette/.hermes/cache/scratch/woodworking-tutorial/dotnet
python harness/audit_sources.py
"$DOTNET_ROOT/dotnet" run --project harness/RichtextHarness.csproj -c Release --no-restore
```

No NuGet packages are needed; project references point only to existing official base DLLs and bundled support libraries. Assembly references use absolute paths to the supplied trusted base installation. The decompiler used was `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/tools/ilspycmd`.
