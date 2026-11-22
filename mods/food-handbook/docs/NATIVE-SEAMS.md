# Native composition evidence — 0.3.0

Evidence is read-only inspected source/metadata, not an executed upstream client. External inspection files are not redistributed.

## Recipe provenance, not output-text surgery

Evidence: `food-handbook-hook-spike/evidence/exact-handbook.cs` lines1753–1840 (grid matching/output generation), 1890–1916 (barrel matching), 1954+ (grinding/juicing/cooking), and addToListUniquely at4620. Target: native `CollectibleBehaviorHandbookTextAndExtraInfo.addIngredientForInfo`.

Exact IL SHA-256: `E2ADD5DAADB951752CBFE9280897F4435734707CF612BEE8CFCD7BA053A46207`.

Its 12 instance-void output-collector calls are fingerprint-locked. Only call1 (generated grid output, current GridRecipe local) and call6 (barrel resolved output, current BarrelRecipe local) are wrapped. Call2 returnedStack and all other collectors remain untouched. Metadata validates ABI and unique recipe-local types; the Harmony transpiler additionally rejects non-12-call shapes. Classification uses exact recipe.Name domain/path AND declared output template. This prevents same-output independently legitimate recipes being removed by deduplication. Unknown names/output patterns or projection errors invoke the original collector.

`woodworking-tutorial/survival/Systems/Recipe/RecipeLoader.cs` LoadRecipe lines122–125 assigns `recipe.Name = assetLocation` only when no explicit name was declared; variants Clone the recipe. Thus source asset path, not optional `code`, is the default identity. These source defaults still need actual client synchronization verification. Policy rows retain declared enabled state and exact source hashes. Native registry matching, quantities, returned stacks and generated outputs are not reimplemented.

The thread-local nested scope captures only selected contributions. Headings/icons are prepared before suppressing native insertion; postfix appends dedicated sections; finalizer restores parent scope. Existing generic ingredient heading is generated only from the remaining native list. Runtime exception/conflict behavior remains a client test requirement.

## Independent review hardening

Startup now SHA-256 gates the complete reviewed ACA assembly (`8f6cb3c873f39cc2e3b898aa538015370608547b6f4d34a0fbdae51ec255406b`) before patch installation; unchanged hook ABI/call count cannot certify helper predicates or generated lambdas. Unknown binaries fail closed.

Exact API decompilation confirms RecipeBase.ToBytes writes `Name?.ToShortString() ?? ""` and FromBytes restores `new AssetLocation(reader.ReadString())`. BarrelRecipe delegates both operations to base. This is decompiled serialization evidence, not a serialization round-trip test or proof of active registry identities.

ACA mixing exclusion requires non-null `CooksInto?.ResolvedItemStack`, not merely a CooksInto object. The addon retains ACA meal widgets and does not reimplement this predicate. Native nested thread-local scopes clean up through finalizers. Both hook orders retain haveText once a section exists; live Harmony order, emitted stack validity, reentrancy, foreign patches and render-time exception behavior still require client acceptance.

## Native attribute headings

Evidence: exact-handbook.cs1166–1187 Grinding; 1189–1241 Juicing;1360–1377 Distillation;1379+ Perish/Dry/Cure/Ripen/Harden transitions. Existing named native sections and all native widgets/callbacks remain.

The actual method is **addProcessesIntoInfo**, not addGeneralInfo. Exact IL SHA-256: `08D01932676FDA0BD552D689FF84150666ECAA6317B6FFFCD03AE8FC4B535101`. Metadata validates ABI and the existing dynamic localization-key prefix `handbook-processesinto-transition-`. At exact-handbook.cs1414 the native code combines resolved EnumTransitionType and time unit with duration. Thus curing/ripening/hardening are already correctly named native process sections, not generic text requiring deletion or a speculative replacement hook. Inspection caught and rejected such an unnecessary adapter before release; no production attribute-rendering patch exists. Native grinding/juicing/distillation and transition widgets, duration calculation and click routing remain original for food and nonfood alike.

Attribute pointers are not unique recipes. Perish remains Spoilage, bakingProperties are oven metadata rather than independent recipe declarations, and nested smeltedStack/pressedStack pointers refer to their owning native mechanisms. `onCuringRackProps` in the saved Ancient Tools saltedmeat compatibility patch contains only a transform; it is neither a recipe nor justification for adding an Ancient Tools integration.

## Known limitations

IL fingerprints reject unknown native versions; a runtime Harmony conflict can still alter instruction composition and needs client testing. Exact source classification does not prove loaded config, wildcard expansion, names synchronized to client, page routing or GUI layout. No upstream assembly was executed to claim those results.
