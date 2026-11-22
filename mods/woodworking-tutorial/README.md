# Woodworking Tutorial

Adapts Vintage Story's early woodworking tutorial to **Immersive Woodworking**: prepare a chopping block, then make firewood. Includes inline item previews, handbook links and English/Polish text.

- Mod version: **1.3.0**.
- Install the built mod ZIP, not this source directory.
- Runtime metadata and translated text: [`package/`](package).
- Implementation: [`src/Mod.cs`](src/Mod.cs).
- Regression tests: [`test/`](test).

## Build from source

Requires .NET 10 and an external Vintage Story installation:

```sh
dotnet build src/KuboczochWoodworkingTutorial.csproj -c Release -p:GameDir=/path/to/game
dotnet run --project test/Test.csproj -c Release -p:GameDir=/path/to/game
```

For installation, package the compiled `KuboczochWoodworkingTutorial.dll` together with the **contents** of `package/` at the ZIP root.

**Do not run `build_mod.py` over this source tree.** It is the retained original 1.0.0 bootstrap generator and would overwrite the newer 1.3.0 implementation. Build the project above instead.
