# Kuboczoch Woodworking Tutorial 1.3.0

Author: Kuboczoch
Game: Vintage Story 1.22.7. Client-side tutorial extension.

## Install
Close the game. Remove the older KuboczochWoodworkingTutorial ZIP from VintagestoryData/Mods and add KuboczochWoodworkingTutorial-1.3.0.zip without extracting. Restart and resume First Steps. No tutorial reset is required.

## Presentation
Two compact objectives remain:
1. Prepare and place a chopping block.
2. Split a log, split a half-log, and collect four firewood.

Both English and Polish now use native rendered item/block previews paired with clickable handbook labels. The first objective references the axe, log and chopping block; the second references the log, half-log and firewood. Open the references by releasing the cursor with Alt and clicking an item label. The examples illustrate a flint axe and oak log; progression does not restrict the player to these example materials.

Tutorial text stays in-world, with short headings and no configuration or compatibility commentary. Saved progress, completion rules and the two-stage structure remain unchanged.

## Verification
75 headless checks pass against the actual game assemblies, covering stage progression, save migration, native VTML preview/link component creation, canonical handbook page targets, release assembly version, and EN/PL layout at widths 260 and 300. See test-v130.txt outside the install ZIP. These checks use substitute world services for item resolution; actual item rendering, click navigation and appearance in a running game still require in-game verification.

## Package
The install ZIP contains only the mod DLL, modinfo.json and namespaced language assets. No original game files are changed. Removal leaves earned tutorial progress intact.
