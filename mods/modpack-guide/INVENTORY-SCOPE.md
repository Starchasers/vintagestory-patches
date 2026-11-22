# Inventory scope and exclusions — 1.3.0

The table scope is every registered handle, grip, binding and treatment mapping supported by the active Toolsmith 1.2.19 installation, plus its maintenance/workshop items and native reforging action. Tool heads and completed tool types remain on native pages; they are not handle/grip/binding alternatives. No inactive optional-mod option is advertised as obtainable.

| Type | Registered options | Equal-stat rows | Unresolved active mapping records |
|---|---:|---:|---:|
| handles | 5 | 5 | 0 |
| grips | 84 | 9 | 14 |
| bindings | 57 | 26 | 29 |
| treatments | 7 | 7 | 4 |

The no-grip, no-binding and untreated baselines are additional rows, not registered collectibles. Headers are not counted as options. Native handle wood variants and cloth colours are attributed states rather than additional parts-mapping IDs. Cycling widgets retain all relevant variants; duplicate widgets for those same stacks are prohibited within a page. Native stations are grouped only across identical stone/orientation mechanics. Sandpaper is explicitly distinguished from an edge sharpener.

## Explicitly omitted active mapping records

These are mappings in active Toolsmith configuration files for which the supplied enabled pack has no matching registered item/block. The exact missing IDs are retained in the machine inventory; they are not quietly lost from the enumeration. Compatibility directories for absent mods are separately excluded by their actual enabled mod IDs.

### Handles

None.

### Grips

`leather-normal-acacia`, `leather-normal-aged`, `leather-normal-baldcypress`, `leather-normal-birch`, `leather-normal-cypress`, `leather-normal-ebony`, `leather-normal-kapok`, `leather-normal-larch`, `leather-normal-maple`, `leather-normal-pine`, `leather-normal-purpleheart`, `leather-normal-redwood`, `leather-normal-walnut`, `buckskin`

### Bindings

`fabricstrips-atleather`, `cordage`, `cord-bamboo`, `cord-bark`, `cord-coconut`, `cord-flax`, `cord-mixed`, `cord-nzflax`, `cord-palm`, `cord-papyrus`, `cord-porcupine`, `cord-reed`, `cord-sinew`, `cord-straw`, `cord-vine`, `cord-larch`, `cord-tuja`, `cord-honeylocust`, `cord-birch`, `cord-brideinwhite`, `cord-alder`, `cord-bearnut`, `cord-willow`, `cord-redwillow`, `cord-poplar`, `cord-aspen`, `cord-eucalyptus`, `cord-kapok`, `cord-sapele`

### Treatments

`plantoil-flax`, `plantoil-walnut`, `woodfinish-flax`, `woodfinish-walnut`

## Grouping safety

Every registered option is covered exactly once. `verify_table_sources.py` re-enumerates source mappings and stats, independently expands registered asset variants, verifies maintenance-tag inclusion and checks the exact partition. Python tests additionally reject unequal-stat grouping and repeated item/family stacks per page. Metal wires have separate rows whenever stats differ, regardless of appearance. Rows are grouped by material/use; the binding tier is an attachment requirement, not a promise that a later row is universally better. Recovery, handle support and capacity remain separate choices.

The guide does not enumerate every dynamically generated tool head, finished tool, or every recipe ingredient as a comparison row. Their native recipe pages are deliberately referenced instead. Static registration and real native callback tests validate destinations for the supplied reconstructed stacks; only a running modpack can prove live creative-stack/recipe enumeration.
