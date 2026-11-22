# Vintage Story patches

Custom gameplay fixes, compatibility patches and handbook improvements for the **Pszczoły** Vintage Story modpack.

## Gameplay and compatibility

- **[AoG Cookery Compatibility](mods/aog-cookery-compat)** — Restores Rustic Cookery braising interactions and sounds on Art of Growing firepits.
- **[Boat Resin Construction](mods/boat-resin-construction)** — Uses one raw resin per seam when building Rough Seas boats, instead of pitch glue. Keeps construction timing and progress unchanged.
- **[Firepit Tinder Return](mods/firepit-tinder-return)** — Returns dry grass when dismantling newly built dry-grass firepits, rather than incorrectly returning straw. Straw-built firepits still return straw.
- **[Pszczoły Tea Balance](mods/pszczoly-tea-balance)** — Reduces Floral Tea and Wild Tea satiety and updates their descriptions, without changing healing effects.
- **[Quiet Rough Seas](https://github.com/Starchasers/vintagestory-quietroughseas)** — Quieter Rough Seas logging, axe-only player hull damage, stronger paddling/sculling, helpful-only boat currents and lightning protection for animals.
- **[Saw Container Guard](mods/saw-container-guard)** — Prevents Saw Chiseling from converting containers and other stateful blocks into chiseled blocks. Empty containers are protected too; ordinary solid wood remains chiselable.

## Handbooks and tutorials

- **[Food Handbook](mods/food-handbook)** — Adds cooking-process explanations to A Culinary Artillery's native handbook recipe sections, without changing recipes or tooltips.
- **[Modpack Guide](mods/modpack-guide)** — A pack-wide mod directory, Toolsmith comparisons and cross-mod process guides for food, farming, workshops, transport and more.
- **[Tutorial Skip](https://github.com/Kuboczoch/kuboczoch-tutorial-skip)** — Lets you skip a stuck tutorial step and saves progress immediately.
- **[Woodworking Tutorial](mods/woodworking-tutorial)** — Adapts the early woodworking tutorial to Immersive Woodworking: prepare a chopping block, then make firewood, with item previews and handbook links.

## Visuals and economy

- **[Visual Mining](mods/visual-mining)** — Client-side mining animations and cosmetic voxel chips, without gameplay changes. Original visual assets credited to SaltyWater and GamingToast.
- **[Coins](https://github.com/Starchasers/vintagestory-coins)** — Hubert's player-issued currencies, banknotes and coin purse, with Vinconomy shop integration.

## Playing with these mods

This repository contains **source code**, not a single installable modpack. Use the built mod ZIP for each mod you want, together with its required game version and dependencies. Individual mod documentation and `modinfo.json` files describe their requirements.

Place installable ZIPs in your Vintage Story `Mods` directory. When updating, replace the old ZIP rather than keeping two versions of the same mod. Restart the game after changing mods. Do not copy the entire repository into `Mods`.

Quiet Rough Seas, Tutorial Skip and Coins are maintained in their linked repositories. Their directories here are Git submodules; their releases and issue trackers remain with those projects.

## Source code

Each mod has its own directory under [`mods/`](mods). To download this collection including the linked projects:

```sh
git clone --recurse-submodules https://github.com/Starchasers/vintagestory-patches.git
```

Build instructions: [developer documentation](docs/DEVELOPMENT.md). Coins is a private repository; fetching that submodule requires access. To fetch only the public standalone mods:

```sh
git clone https://github.com/Starchasers/vintagestory-patches.git
cd vintagestory-patches
git submodule update --init --recursive -- mods/quiet-rough-seas mods/tutorial-skip
```

## Credits

Mods and patches by **Kuboczoch**; **Coins** by **Hubert**; **Quiet Rough Seas** by **Hubert and Kuboczoch**. Individual projects retain their existing credits and license notices. Vintage Story and third-party dependencies are not included.
