# Saw Chiseling 0.1.2 container-conversion investigation

## Exact provenance

- Pack supplied for investigation: `modpack.zip`, entry `mods/SawChiseling_0.1.2.zip`.
- Its `modinfo.json`: modid `sawchiseling`, name `Saw Chiseling`, author `SaltyWater`,
  version `0.1.2`, universal code mod.
- Extracted original `SawChiseling.dll` SHA256:
  `6484c676aa3c7e52d71b14462c8c6fb90872176a23a192b4d1f3f348ec7eacc6`.
- Inspected using ILSpy, and exercised against actual Vintage Story **1.22.7** API,
  survival and Harmony assemblies. Upstream/game assemblies are not in this repository
  or deliverable. The unmodified root ModTemplate is unrelated to this addon.

## Root cause

The destructive conversion belongs to `SawChiseling.SawChiselPatch`, not the vanilla
saw's ordinary operation:

1. `HandleInteractStart(...)` resolves the selected block, tests for a saw and
   `EnumBlockMaterial.Wood`, then enters conversion after upstream eligibility checks.
   Client-side handling sends a placement request; server-side handling can place directly.
2. `TryServerPlaceWithSaw(IWorldAccessor, IPlayer, BlockPos)` repeats target resolution,
   wood and `CanSawChisel` checks. It is also used by the mod's server packet handler.
3. `TryResolveTargetBlock(IWorldAccessor, BlockPos, out Block)` just retrieves the block
   (except already-chiseled/microblock cases), with **no inventory/container/state check**.
4. `CanSawChisel(...)` checks claims, reinforcement, creative mode, decor, resistance
   and cube/DrawType eligibility. It does **not** exclude containers or block entities.
   Creative mode returns true before the shape restriction.
5. `PlaceChiseledBlock(IWorldAccessor, BlockPos, Block sourceBlock)` obtains
   `game:chiseledblock`, calls `world.BlockAccessor.SetBlock(...)`, gets the replacement
   `BlockEntityChisel`, and calls `WasPlaced(sourceBlock, null)` and `MarkDirty(...)`.
   It neither preserves nor migrates the original inventory. Once SetBlock executes,
   even failure to obtain the new BE occurs **after** the destructive replacement.

The native 1.22.7 `assets/survival/blocktypes/reed/reedchest.json` explains why a reed
chest enters the wood path: `class: BlockGenericTypedContainer`,
`entityClass: GenericTypedContainer`, and `blockmaterial: Wood`. It is an inventory
container, not a plain wooden cube. Whether a particular in-game interaction satisfies
shape restrictions also depends on mode/runtime shape configuration. The user's
specific rendered-world event was not replayed in a full game instance.

## Fix boundaries

A separate universal addon patches three exact methods, rather than replacing or
redistributing upstream code:

- `TryResolveTargetBlock`: deny protected state with false and a null output. This
  is shared by held client interaction, server interaction, held-use and server requests.
- `HandleInteractStart`: protected selections return true (the return contract of
  upstream's Harmony prefix), preserving the native handler and avoiding animation,
  placement requests, conversion and interaction suppression.
- `PlaceChiseledBlock`: independent final guard before any `SetBlock`, including
  direct/reflection calls that bypass the resolver.

Detection is native type/behavior/interface + declared/actual block-entity state,
not names or nonempty inventory checks. Empty and full containers receive identical
protection. All unknown BE state is conservatively protected. This also intentionally
excludes stateful non-containers. Lookup errors fail closed. Existing ordinary chisel
editing is exempt from resolution only, never from direct conversion.

The mod does not globally patch SetBlock, alter container inventories or write saves.
Upstream optional absence is a no-op; installed signature incompatibility throws a
clear startup error after validating all signatures, before patching any. Installation
failure rolls back only prefixes owned by this Harmony ID. Future upstream methods
or entirely external inventory storage need a separate compatibility review.

## Executed verification

Production Release build: **0 warnings, 0 errors** using .NET SDK **10.0.404**.
Native regression harness: **290 assertions passed** against the exact original DLL,
including production client/server `Start` discovery, installed protection and `Dispose`.
A separate fresh process with upstream absent passed **5 startup/no-op assertions**.
Package regression suite: **8 tests passed**.

The harness executes real upstream resolver, server eligibility and placement methods
before and after production Harmony patch installation. Unpatched empty/full native
reed/wood/labeled/reinforced-type/modded/interface-container fixtures reach actual
fixture-accessor `SetBlock` replacement. Patched fixtures retain original block/BE,
native inventory, ItemSlot and ItemStack identity/quantity/serialized bytes; both
client/server held handlers resume native handling without consuming the event.
Additional cases cover behavior-only containers, a microblock with an inventory BE,
custom stateful BEs, null/missing BEs with declared EntityClass, failed lookups,
nonwood, null targets/sources, and unchanged ordinary plank/log conversion.

Fixtures instantiate native types but **do not load the chest JSON assets into an
initialized world**. Native world/accessor/claims interfaces are proxied, and new
chisel BE initialization/dirty notification are scoped observation seams only.
A reinforced-type container is not a running block-reinforcement subsystem. These
limits are detailed in `tests/README.md`; no mesh, networking, GUI, save/reload or
full dedicated-server smoke success is claimed. Run the README manual checklist
on a disposable world copy before deployment.
