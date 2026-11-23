# Building the mods

Each mod has its own directory under `mods/`, with its source, assets and available build tools and tests.

Quiet Rough Seas, Tutorial Skip and Coins are pinned Git submodules. Fetch them with `git submodule update --init --recursive`. Coins is private and requires repository access. Follow each linked project's own build documentation.

## Prerequisites

Compiled mods require .NET 10 and a separately obtained Vintage Story installation/SDK, generally 1.22.7 for these versions. Additional dependencies are declared in each mod's `modinfo.json` and project files. Game assemblies and third-party mod assemblies are not included.

Some mod scripts reference their original local workspace. Supply your own SDK and pack paths when rebuilding. Existing test reports describe earlier runs, not a fresh build against your installation.

## Build commands

Run commands from the relevant mod directory.

- **AoG Cookery Compatibility / Pszczoly Tea Balance:** content-only mods. Package their assets and `modinfo.json` at the ZIP root; no compilation needed.
- **Boat Resin Construction:** build `mod/KuboczochPitchGlueCompat.csproj`. See `Directory.Build.props`, `package.py` and the mod README for SDK/pack parameters.
- **Inventory Hover Cleanup:** `python tools/package.py --game-path /path/to/game --dotnet /path/to/dotnet`. Production Harmony tests: `dotnet run --project tests/Tests.csproj -c Release -p:GamePath=/path/to/game -p:DeadPath=/path/to/dead`; append `-- --red` for unpatched crash reproduction. Requires exact VS 1.22.7 and Dead 2.9.8 for tests; see its README for packaging tests and validation limits.
- **Firepit Tinder Return:** `python tools/build.py --vs /path/to/game --pack /path/to/modpack.zip`.
- **Food Handbook:** set `GAME_PATH=/path/to/game` and `ACA_PATH=/path/to/ACulinaryArtillery.dll`, then run `python tools/build.py`. SDK-free policy tests: `dotnet run --project tests/Tests.csproj -c Release`.
- **Modpack Guide:** `dotnet build client/ToolsmithFamilySlideshows.csproj -c Release -p:GameDir=/path/to/game`. See its README for content generation and testing.
- **Visual Mining:** `dotnet build src/KuboczochVisualMining.csproj -c Release -p:GameDir=/path/to/game`, then `python package.py`.
- **Woodworking Tutorial:** follow its README. Build the current project directly; the retained `build_mod.py` is an older bootstrap that would overwrite newer source files.
- **Saw Container Guard:** `python tools/package.py --game-path /path/to/game`. Packaging tests: `python -m unittest discover -s tests -p 'test_*.py' -v`.

## Credits and licensing

Individual mods retain their existing license notices and third-party asset credits. No blanket license is applied to the collection. Obtain game SDKs and external mod dependencies separately.
