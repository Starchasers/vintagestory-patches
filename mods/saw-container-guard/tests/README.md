# Native headless regression harness

Run against a local Vintage Story 1.22.7 SDK and the **original** SawChiseling.dll:

```sh
/path/to/dotnet run --project addons/SawContainerGuard/tests/NativeRegression.csproj \
  -p:GamePath=/path/to/vintagestory \
  -- /path/to/original/SawChiseling.dll
```

Run a separate fresh process to verify optional-upstream startup is a no-op:

```sh
/path/to/dotnet run -c Release --project addons/SawContainerGuard/tests/NativeRegression.csproj \
  -p:GamePath=/path/to/vintagestory -- --absent
```

The main suite also invokes the production ModSystem `Start` and `Dispose` on native
client/server ICoreAPI proxies, checking assembly discovery, installed protection,
installation logging and removal of its own patches. This is not the full game mod loader.

This executable uses the actual SDK Block, BlockContainer, BlockGenericTypedContainer,
BlockLabeledChest, BlockGroundStorage, native container/chisel block entities,
InventoryGeneric, ItemSlot and ItemStack types. No game API replacement assembly or
stand-in declarations are compiled. Modded subclasses and an interface-only BE are
local test extensions of those actual native types. Reed/wood/reinforced fixtures use
the shared native generic typed-container implementation; they are not JSON-loaded
asset definitions or proof of a rendered/in-game chest UI.

Before installing the production guard, empty and full fixtures execute the exact
private/internal upstream methods by reflection and demonstrate destructive SetBlock
replacement. After install, the same methods check denial and unchanged original
block/BE/inventory/stack identity, quantity and serialized bytes. Both client and
server HandleInteractStart calls must return true with handling unchanged, preserving
native interaction. Other cases cover custom BEs, declared EntityClass without BE,
BlockContainer without BE, lookup failure/nulls, nonwood, existing chisel/microblock
resolver exemptions, absent-upstream installation and normal planks/log conversion.

## Headless boundaries

- DispatchProxy implements the actual world/accessor/claims interfaces; its SetBlock
  explicitly models replacement and records the mutation. This is not a running
  chunk manager or an initialized game server.
- Native IPlayer in 1.22.7 contains nonpublic interface methods that DispatchProxy
  cannot implement (runtime TypeLoadException). The upstream-supported null player
  path is used for server conversion; held calls use a real native EntityPlayer.
- To avoid pretending an initialized renderer/mesh/world exists, scoped Harmony
  observation prefixes intercept native BlockEntityMicroBlock.WasPlaced and
  BlockEntity.MarkDirty **only on the fixture's replacement BlockEntityChisel**.
  They record the exact upstream calls/source while skipping native initialization.
  Upstream resolver, eligibility, placement and accessor SetBlock execute normally.
- No assertions claim successful native mesh creation, chest GUI opening, network
  delivery, save/reload persistence or full in-game behavior.
