# Stopper-return investigation

## Provenance

Report: [Starchasers/vintagestory-patches #3](https://github.com/Starchasers/vintagestory-patches/issues/3), ceramic stopper disappearing when opening a bottle with a matching stopper in the offhand.

Inspected the actual `ACulinaryArtillery_2.0.0-dev.29.zip` from the supplied modpack snapshot, not a guessed implementation. Its manifest declares `aculinaryartillery`, version `2.0.0-dev.29`, game dependency `1.22.3`. SHA256 of its `ACulinaryArtillery.dll`:

`8f6cb3c873f39cc2e3b898aa538015370608547b6f4d34a0fbdae51ec255406b`

Also tested the actual DLL from the [official dev32 archive](https://moddbcdn.vintagestory.at/ACulinaryArtillery_2_2cce38a4a95049ce37f67fe08fa409cc.zip?dl=ACulinaryArtillery_2.0.0-dev.32.zip), linked by the [ACA release API](https://mods.vintagestory.at/api/mod/aculinaryartillery). DLL SHA256:

`dfea2335e2f61616974293c7ee03d955366e318c959252f475597ef2b0b9f471`

Decompiled both published assemblies with ILSpy. Source repository: [GastronomistGuild/ACulinaryArtillery](https://github.com/GastronomistGuild/ACulinaryArtillery). Published assemblies, not moving source HEAD, are the compatibility/test targets. No third-party source or binaries are redistributed here.

## Exact cause and scope

Target method: `ACulinaryArtillery.BlockStoppableContainer.OnHeldInteractStart(ItemSlot, EntityAgent, BlockSelection, EntitySelection, bool, ref EnumHandHandling)`.

In both published versions, the uncorking branch:

1. Reads `GetStopper(itemslot.Itemstack)` to check offhand collectible compatibility. An empty offhand is accepted, but a nonempty one must have the **identical collectible object** as the nested stopper (`ceq` in IL). Different stopper types fail this early guard, so the later failed-transfer ground-drop fallback is never reached. The previous clone-only patch preserved this restriction.
2. Reads it again into the removed-stopper local. `GetStopper` uses native `BlockContainer.GetContents` and returns the nested `ItemStack` directly, not a clone.
3. Splits/replaces the container, removing its stopper from the new open bottle. When splitting a held stack, the **remaining bottles retain the original nested stack reference**.
4. Transfers the removed-stopper local through `new DummySlot(stopper).TryPutInto(world, offhandSlot, 1)` or spawns it if that fails.

Native `ItemSlot.TryPutInto` / `CollectibleObject.TryMergeStacks` decrement the source stack on a successful transfer. Because that source aliases the remaining bottles' contents, they now contain a **zero-sized stopper**. A later uncork can transfer/drop nothing. The initial transfer succeeds: the damage is to subsequent bottles in the held stack. An empty offhand can also trigger the aliasing bug; it is not ceramic-specific. A fresh single bottle with a matching offhand did not lose its stopper in this harness. The reported broader single-bottle wording is not claimed fully reproduced.

Version **1.0.1** replaces only that first-read collectible comparison with a helper: reference equality still succeeds, and different collectibles succeed only if **both** carry `isLiquidContainerStopper: true`. Non-stopper routing is preserved. ACA then attempts the native transfer, which rejects different collectible types, and drops the removed stopper through its existing fallback. The patch also retains the clone call immediately after the **second** `GetStopper` read. It does not replace the method, alter other getters, patch native slots globally or edit provider files. A signature lookup, exactly-two-read/store-local guard and null-safe collectible-comparison IL check reject unexpected provider shapes. Harmony ownership enables clean unpatching on disposal. It runs on both sides to preserve identical local interaction logic; networking remains ACA's responsibility.

No normalization of nested stack sizes is performed. Already-corrupted stoppers are not healed, to avoid silently inventing items or changing saved data.

## Real execution results

VS **1.22.7**, .NET **10**, real ACA **dev29** and **dev32**, separately executed:

- Each ZIP discovers **47** usable variants: bark 1, ceramic 10, glass 11, metal 12, wood 13. Raw ceramic lacks the usable-stopper flag. Optional cross-mod patches are not applied. Inheritance, variant expansion, max stack size and attributes come from actual provider assets, not a ceramic-only fixture.
- Each mode runs **4,796 cases per provider**. Every variant crosses empty, matching two/near-full/full, all 46 different stopper variants and an unrelated non-stopper, with one and three held bottles opened sequentially; two additional cases verify fail-closed IL guards.
- Original `--red`: **4,512 failures**, nonzero exit, in each provider. Both mismatch restriction and nested-stack aliasing reproduce.
- Previous behavior `--clone-only`: **4,324 failures**, nonzero exit, in each provider. The clone prevents aliasing but every different-stopper pairing still blocks opening.
- Current patched: **0 failures**, exit zero, **4,796 passed per provider**. Exact offhand/drop counts, offhand identity under mismatch, nested stack size/reference isolation and bottle/stopper conservation are checked after every opening.
- Packaging policy tests cover repeatable allowlist archives, foreign DLLs, symlinks, disguised executables, private paths and non-DLL payloads. They use explicitly synthetic packaging bytes, never a fake production assembly.

The native-method harness constructs real ACA blocks, VS `ItemStack`, `DummySlot`, `ItemSlotOffhand`, nested contents and `EntityPlayer`. Only world lookup/spawning and inventory-manager services are mocked. IPlayer has an internal SDK interface member; a test-only dynamic implementation supplies its inventory manager without modifying the SDK. No test helper ships in the mod DLL.

These tests establish the aliasing bug, early mismatch prohibition and narrow managed repair against both published providers. They do not establish asset-loader startup, complete pack integration, rendering, sound, client/server synchronization or a user game reproduction. Game testing remains pending.

## Would upgrading fix it?

Not by itself for the verified trigger: **dev32 fails the same unpatched native-method regression**. Its advertised ghosting fix ([PR #247](https://github.com/GastronomistGuild/ACulinaryArtillery/pull/247), [commit b825ae4](https://github.com/GastronomistGuild/ACulinaryArtillery/commit/b825ae4cd99016a6753e23a432e4434ac0288f68)) server-gates returning transformed containers, not the stopper-local transfer. Earlier attribute-cleanup improvements likewise do not detach this nested item.

[Expanded Foods Core](https://mods.vintagestory.at/expandedfoods) dev18 is content-only, not a replacement for ACA's method; its manifest requires ACA dev30 or newer. Neither an EF upgrade nor a pack-wide upgrade is required by this standalone patch, and no full upgrade compatibility is asserted.
