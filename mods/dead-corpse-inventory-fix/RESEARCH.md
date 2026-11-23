# Corpse inventory hover cleanup investigation

## Exact inputs and citations

- User crash: VS 1.22.7, Dead 2.9.8; NRE at
  `GuiElementItemSlotGridBase.OnMouseLeaveSlot` line 925, via `GuiDialog.OnMouseLeaveSlot`
  -> `InputAPI.TriggerOnMouseLeaveSlot` -> grid `OnMouseMove` line 918 ->
  `Dead.Gui.GuiDialogCorpse.OnMouseMove` line 347. Harmony ID `spyglass` is present,
  but that is not evidence of causal responsibility. Other Rough Seas stacks were
  excluded from this investigation.
- [Official VS 1.22.7 server distribution](https://cdn.vintagestory.at/gamefiles/stable/vs_server_linux-x64_1.22.7.tar.gz).
  Its API constant `GameVersion.OverallVersion` is exactly `1.22.7`.
  API DLL SHA256: `034283e7e9d98eae45ee63005576fd89badc3c995b531cc4c3fe46f3eb2d3296`.
- [Dead official release metadata](https://mods.vintagestory.at/api/mod/dead),
  [Dead page](https://mods.vintagestory.at/dead), release 66117, version 2.9.8.
  [Exact release ZIP](https://moddbcdn.vintagestory.at/dead_2.9.8_af24a1471c5cf3e20b3d11fe479575cc.zip).
  ZIP SHA256: `f43e4beef26da74dd0eda5602694f333da4fb70b7468f9b5bc8a57b1d312fc9f`;
  Dead.dll SHA256: `4b343b8e9e6bdfb54251ed876709b01dca48db1ffde5febad8e80239b2be0541`.
  Metadata has no published source-code URL. Exact implementation was inspected
  locally with ILSpy 11.2.0.10050; no decompiled source is redistributed.
- [Official public API source](https://github.com/anegostudios/vsapi/blob/40562b745918fb6c84088382ecef99d5866c9c07/Client/UI/Elements/Impl/Interactive/Inventory/GuiElementItemSlotGridBase.cs)
  is useful context, **not** a substitute for the exact release DLL. Existing local
  `sdk122` actually identified as 1.22.0 and was rejected for final validation.

## Precise failure mechanism

The 1.22.7 receiver tests `hoverInv == slot.Inventory` without checking `slot`.
The crashing null is the **ItemSlot argument**, not an empty Itemstack. The sender
re-resolves `inventory[hoverSlotId]` at leave time rather than keeping the object
that was entered. Both the outside-parent and inside-parent/no-hover branches can
send null. The receiver exception interrupts event delivery and the subsequent
native `hoverSlotId = -1` assignment; simply catching the exception is not cleanup.

Dead 2.9.8's `GuiDialogCorpse.OnMouseMove` calls the base dialog method and only
then applies character rotation. It contains no special failing leave logic.
`InventoryDeadBackpacks` combines four bag slots with `BagInventory.Count` bag
contents. Its indexer explicitly returns null for indices outside current `Count`.
`OnItemSlotModified` reloads bag contents when the changed slot is not a bag-content
slot, and the corpse dialog's backpack-modified handler can rebuild the GUI. A
previously valid hovered bag-content index can therefore disappear. This is an
exact, reproduced route to the reported crash, **not proof of the player's precise
mouse action or which mod initiated the resize**. No save/replay was supplied.

## Why retain the entered slot instead of swallowing the NRE?

The original slot still identifies the inventory to leave listeners even after
removal from the current slot array. Sending it allows existing receiver cleanup
and downstream tooltip listeners to execute. Normal non-null leave arguments are
unchanged; listener exceptions still propagate. Only three event-dispatch call sites
in one native GUI method change. The patch does not globally suppress null
references, alter input dispatch ordering, mutate item stacks or blame other mods.

## Recorded validation

On the official 1.22.7 API and Dead 2.9.8 DLLs: red harness reproduced the native
receiver NRE in both native mouse-leave branches. Green applied the production
Harmony transpiler and passed 29 assertions, including healthy paths. Two Python
packaging tests verify byte-for-byte deterministic allowlisted output and rejection
of symlinked documentation. See README for commands and rendered-gameplay limits.
