# Root index correction — 1.3.1

The root had been incorrectly oriented as a beginner tutorial. Restored its mod-directory identity in both regeneration paths. No mod coverage was missing: all 75 existing pages and category destinations remain. The sections below document the unchanged 1.3.0 implementation. Current execution evidence: `test-results-1.3.1.txt`.

# Diagnosis and revision history: 1.1.0 → 1.3.0

## Current 1.3.0 table revision

The later request supersedes the older supporting-page length cap: keep a short first-tool entry, but expose every supported option in real three-column type tables. Native API inspection and a fail-first headless test established that 1.22.7 has no supported HTML/VTML table widget. A narrowly scoped presentation component now measures native richtext in three aligned cells, derives row height from wrapping, and forwards translated mouse events and native interactive rendering. The existing attributed-family approach is retained.

The six stable pageCodes remain under the existing Pszczoly index. Titles use the exact ASCII `Toolsmith -` prefix; the entry preserves `Pszczoly Guide to Toolsmith` identity. Grips and bindings have exclusive separate pages and exact matching navigation labels. Plain localized titles are audited for raw entities; authored table text contains no double-escaped entities. This is checked at title/localization and native VTML-parser boundaries rather than globally unescaping arbitrary markup.

A dedicated plain-reference converter supplies explicit native slideshow callbacks for ingredients and stations, using the same real page-code function as the attributed families. Semantic stack references are unique per page. Recipe/guide anchors are checked separately. The final source audit reconciles both present and missing mapping IDs against enabled assets, so a missing inventory row cannot pass merely by comparing an inventory to itself.

The initial complete-table tests found wrong preliminary customization eligibility and missing professional drying multipliers. These were corrected against exact parts mappings and static CIL, not inferred from names. Sandpaper was added to the maintenance reference because it carries that native tag, with explicit explanation that it crafts handles rather than sharpens edges. See the current `NUMERIC-EVIDENCE.md`, `INVENTORY-SCOPE.md` and `test-results-1.3.0.txt`.

Actual final-page native geometry/callback tests pass across narrow widths and multiple GUI scales. No graphical game client is installed; no GPU/live GUI verification is claimed. The following sections retain the earlier 1.2.0 investigation as historical context.


## Before correction

The acceptance test was first run against the unchanged 1.1.0 install ZIP, before the release builder existed: `GUIDE_ZIP=dist/KuboczochModpackGuide-1.1.0.zip python -m unittest test_progression -v`. `red-tests.txt` records nine assertion failures across six tests: wrong entry title, overlong pages, no compact family slideshow, attributed JSON-filled links, absent numeric comparisons, absent progression. The original source directory was copied and never edited.

Static inspection of exact 1.22.7 `VtmlUtil`, `GuiHandbookTextPage`, `GuiDialogHandbook`, and `CollectibleObject` identified distinct navigation/rendering risks:

1. Native item-page names omit the `game:` domain. A syntactically valid `handbook://item-game:...` is not the native registered name.
2. Several Toolsmith collectibles have **only attributed creative/handbook stacks**. A plain `item-toolsmith:handle` or `item-toolsmith:fabricstrips-leather` is not a registered destination. A correct `PageCodeForStack` calculation alone does not establish registration.
3. Ordinary `<itemstack>` ignores stack-attribute JSON and creates new **plain** stacks. It cannot display a wood-species handle or colour-specific strip by itself.
4. `GuiHandbookTextPage.Init` uses ordinary VTML conversion with a null link callback; native `<itemstack>` creates a slideshow with **no onStackClicked callback**. Decorative icons therefore do not navigate. This is not fixed by a valid adjacent href.
5. The original catalogue exposed all 85 native examples plus large material inventories. A correctly rendered exhaustive catalogue still fails the requested beginner learning-path/length constraints.

The former harness checked parser round-tripping and `PageCodeForStack`, but did not run the dialog's registration method. Raw single-quoted JSON hrefs can round-trip in the native parser; this investigation does **not** claim that single quotes alone caused failures, or invent an observed GUI failure.

## Reproduction during integration

The first native-registry run of the new content failed on `handbook://item-toolsmith:fabricstrips-leather`, even though the code exists. The game registers colour-attributed leather-strip pages instead. That run demonstrated why plain-code validation is insufficient. Handles and leather strips were changed to attributed-family components instead of fake generic anchors.

## Correction

The builder replaces all six Toolsmith catalogue pages, keeps their stable custom page codes, and names the entry exactly **Pszczoly Guide to Toolsmith**. Ordinary game links strip the `game:` domain. Attributed families now carry real inventory snapshot attributes into the native slideshow through a narrowly scoped client tag converter. Its explicit click callback computes the displayed stack's native page code and dispatches the handbook protocol. The archive contains no attributed synthetic hrefs on the progression pages.

Registration verification now deserializes actual pack/native/custom guide asset definitions and reconstructs native handbook stack snapshots; **the actual base-game `GuiDialogHandbook.LoadPages_Async`** populates the dictionary. All progression anchor destinations and all selected attributed-family click destinations are checked against that populated dictionary. Separate tests invoke actual native component `OnMouseDown` for every supplied snapshot.

## Boundaries

Downloaded mod assemblies were statically decompiled only, never executed. The registration input is a fixture derived from the shipped assets and creative-stack attributes; it is not the result of a running full-modpack `GetHandBookStacks` pass. No full game GUI was launched. This distinction is deliberate: the evidence exceeds an isolated page-code check without claiming live initialization/rendering proof.
