# Kuboczoch Food Handbook 0.3.1

**Author: Kuboczoch. Client-only; source-policy implementation, runtime acceptance outstanding.**

For the inspected Vintage Story 1.22.7 / A Culinary Artillery 2.0.0-dev.29 / Expanded Foods 2.0.0-dev.17 bundle. No server/gameplay/recipe/global catalogue changes, upstream execution or upstream redistribution.

## Item-page sections

- Existing ACA registry contributions move into **Simmering**, **Kneading**, **Mixing** (or combined Simmering / Kneading). Bone is one example, not an item-specific rule.
- Food preparation from exact EF grid declaration identities moves into **Preparing / Crafting (food)**.
- Exact EF barrel declaration identities move into **Fermenting**, **Preserving** or **Soaking**, according to source operation. Vinegar/pickled eggs, lime eggs and pickled vegetables are preservation; soy preparation is soaking; yogurt, cultured milk and vinegar making are fermentation.
- Native **Grinding**, **Juicing**, **Distillation**, **Drying**, cooking/smelting and spoilage presentation stays native. Source/API inspection confirms native transitions already have type-specific localized headings and durations (`handbook-processesinto-transition-<type>-<timeunit>`), including curing/ripening/hardening; these are retained, not duplicated or renamed. Nonfood wax/hides/equipment and unrelated-mod attribute pages stay unchanged.

Migrated contributions are not also copied into Ingredient for. Native recipe matching and resolved output generation happen before interception. A grid returnedStack is never intercepted: returned containers/byproducts remain native. Independently legitimate crafting, alloy, clay, knapping, smithing, meal, grinding and other ingredient relations remain, even if their output equals a migrated food output. Created by remains native.

Two narrowly gated Harmony patches: ACA's helper composition seam and native grid/barrel output collector callsites. Existing native attribute rendering is inspected/validated, not patched. Native method IL fingerprints, ABI, local types and contribution count are checked. Unknown native versions/recipe identities retain native presentation. No translated-text deletion, rendered-heading searching, GUI-list removal or global recipe registry mutation. Icons use concrete resolved stacks and provider-first destination routing.

## Coverage evidence, not runtime certification

[Primary ledger](docs/recipe-ledger.json) and [native operation ledger](docs/native-operation-ledger.json) retain individual source paths, hashes, inventory rows, dispositions and unresolved runtime status:

- 626 existing ACA-registry declarations: 203 simmering, 420 kneading, 3 mixing.
- 235 native EF source declarations: 232 food candidates mapped; two nonfood brazier recipes excluded; one More Animals output excluded as unrelated-mod scope. These are 205 grid + 30 barrel declarations, not live recipe totals.
- 429 EF/ACA/Chef's Tricks attribute pointers: 428 mapped to existing native mechanisms, including nested/overlapping pointers and spoilage; one Ancient Tools rack transform excluded as visual-only metadata, not a curing recipe.
- 330 EF patch operations explicitly distinguished from recipes. The adapter reads the effective existing registries, not the patches as if they were recipes.

No automatic provider expansion. No real client was available: active config/patch effects, wildcard expansion, client synchronization of recipe names, runtime GUI identities, Harmony startup/detours, layout, clicks/cache/unload remain unverified. Synthetic policy projections are not live recipe tests. See [blockers](docs/REFINEMENT-BLOCKERS.md), [seam evidence](docs/NATIVE-SEAMS.md), [verification](docs/VERIFICATION.md).

## 0.3.1 changelog and heading links

- Shared Inline output geometry remains unchanged: 40px concrete icons, 2px leading and 3px trailing ClearFloat spacers. Trusted API geometry/input harness covers 84 width/count/scale cases; this is not client acceptance.
- Simmering links to `handbook://simmeringguide`; Kneading/Mixing to `handbook://kneadingmixingguide`. Combined process-3 retains its localized parent, two independently linked localized subheadings, and one shared output row. Preparing/Fermenting/Preserving/Soaking stay plain: no exact guide match was proven.
- **Correction to preliminary review:** exact 1.22.7 `AddHeading` has FOUR parameters, not an optional link. Production compilation and metadata prove this. Local `ProcessHeadings.AddHeading` supplies an optional-link wrapper, preserving native plain handling and native 14px spacing, bold small font/newline; linked headings use the supported `LinkTextComponent.Href`. No upstream edits or patches were added.
- Heading policy has RED/GREEN evidence in `docs/heading-tdd-red.txt` and `docs/tdd-green.txt`; metadata checks lock the actual native API. Geometry helper and both output callsites remain unchanged apart from ACA heading selection.

## Missing references: broader than compote, not all fixed

[Bounded reference ledger](docs/missing-reference-ledger.json) retains 108 source definitions, seven unique meal codes and 21 explicit item-output definitions. All seven native title lookups have upstream en/pl translations. All 108 runtime registrations remain unresolved: saved source candidates do not prove enabled recipes, live pages, generated pie codes, provider routing, asset load order or optional dependencies. Compatibility meal patches are not independently registered meals. One unrelated Firestarters locale parse error is retained.

The user's literal `meat-recipe-compote` observation is preserved, not silently normalized. Exact native meal-page source instead uses `mealrecipe-name-` + recipe.Code, with route `handbook-mealrecipe-` + recipe.Code. Upstream already supplies `game:mealrecipe-name-compote`: English `Compôte`, Polish `Kompot`. Jam is a separate recipe, not a compote fix. Pie title lookup explicitly uses `pie-<code>-perfect`; that is intentional and not evidence of another broken dish lookup. No evidence justified an additional naming fix; no guessed locale overrides or registry changes were made. Actual user-visible missing references beyond compote remain a runtime investigation, not a claim of all-fixed coverage.

### Reproduce the source-reference audit

Requires Python `json5`, the saved exact-pack provider inventory, the original evpack and matching game assets (external inputs, not redistributed). Paths are explicit arguments; output defaults to this source tree's `docs/missing-reference-ledger.json`, independent of stale scratch-directory names:

```sh
python tools/food-handbook-reference-audit.py --inventory /path/to/native_guides_all_providers.json --evpack /path/to/pack.evpack --game-assets /path/to/vs/assets --output /path/to/reference-ledger.json
```

The packaged audit is copied from the original saved audit with only argument-driven input/output paths. Its reproduced JSON was compared structurally against the saved ledger and matched. Inspect `MISSING []` as a bounded source-translation result only, never as proof that every reported reference works in-client.

## Build

```
DOTNET=/path/to/dotnet python tools/build.py
```

Uses exact existing references, builds/tests original code, metadata-checks upstream, exercises trusted game API layout/input without starting the game or ACA, and checks archive contents. See `docs/LAYOUT-0.3.1.md` for the reproduced geometry bug and remaining client acceptance. Outputs `dist/KuboczochFoodHandbook-0.3.1.zip` and `dist/KuboczochFoodHandbook-0.3.1-source.zip`. Nothing was deployed or published. For eventual client installation, remove the previous addon version; no server install.
