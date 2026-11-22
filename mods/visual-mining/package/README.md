# Kuboczoch Visual Mining 1.0.4

Private, standalone **client-only** cosmetic mod for Vintage Story **1.22.7 / .NET 10**. Author: **Kuboczoch**. Original reusable visual assets by SaltyWater and GamingToast; retained here for the owner's private use. No server install, networking, block/durability/resistance changes, chiseling, custom drops, or mining-permission bypass.

## Install and configure

Replace the previous Kuboczoch Visual Mining ZIP in your client `Mods` directory with `KuboczochVisualMining_1.0.4.zip`. Keep only one version. Remove Immersive Mining: this replacement deliberately refuses to activate alongside it and cannot undo another mod's gameplay changes.

On first client start, the mod creates `ModConfig/kuboczochvisualmining.json`. Existing files are not overwritten. Edit the existing nested `Enabled` fields, then **restart the client**; there is no live config reload:

```json
{
  "Pickaxe": { "Enabled": true },
  "Axe": { "Enabled": true },
  "Shovel": { "Enabled": true }
}
```

This is a minimal valid example, not a request to replace your existing settings. Each tool is independently enabled by default. Set its `Enabled` to `false` for normal vanilla presentation: original first-/third-person animation, including occupied offhand; vanilla hit sounds and crack decals; no mod chips, particles, sound replacement, or shake. Other tools retain their own settings. Gameplay remains vanilla either way. Missing fields use existing defaults in memory; explicit `false` and all existing user settings remain in the file. Malformed configs are logged and left untouched while defaults are used.

Other existing options are unchanged: global `VoxelChips`, `ImpactParticles`, `ImpactSounds`, and nested tool `AnimationSpeed`, `ContactFrame`, `CameraShake`. Disabling a global effect disables that effect for every enabled tool; setting a tool's `CameraShake` to `0` disables its mod shake. Tool disable bypasses all mod presentation regardless of those settings. The generated file includes these defaults; no extra toggle schema was introduced.

## What changed in 1.0.4

The three tool switches already existed. This release makes their full vanilla fallback reliable when switching tools: it clears old damage/chip/contact state and stops only `kvm-*` animations in both explicit player camera managers, never vanilla held-hit or movement animations. Enabled tools keep their animations while retargeting or waiting for fresh damage observations. Config loading no longer rewrites existing files.

The accepted 1.0.2 wood/dirt/pickaxe pipeline and 1.0.3 peat/clay `TopsoilTesselator` handling remain. Cosmetics observe actual vanilla damage and commit on contact; custom/unsupported meshes retain vanilla cracks. Runtime per-entity animation registration still avoids the previous metadata-loss bug; offhand-safe clips contain no left-arm tracks. Guest-mode/world permission restrictions are normal game behavior, not bypassed.

## Build and verify

Use your own Vintage Story 1.22.7 client directory for references; no game DLLs are distributed. With .NET 10 installed:

```sh
dotnet build src/KuboczochVisualMining.csproj -c Release -p:GameDir=/path/to/vintagestory
python tests/run_verification.py --dotnet dotnet --game /path/to/vintagestory
python audit.py
python package.py
```

Optional historical red regression (requires the privately retained original 1.0.3 install ZIP):

```sh
python tests/run_toggle_red.py --dotnet dotnet --game /path/to/vintagestory --legacy-install /path/to/KuboczochVisualMining_1.0.3.zip
```

See `TEST-EVIDENCE.txt`, `REVIEW.md`, and source `evidence/1.0.4/` for real headless results. Tests exercise engine animation/getter routing, all eight tool-toggle combinations, offhand/FP/TP paths, sound suppression, chip observation, decal gating, cleanup and 57 real renderer fixtures. Headless tests cannot prove rendered appearance/audio or multiplayer in-game acceptance; those still require a client session. Existing limitations remain: a block broken before its cosmetic contact frame produces no fabricated post-break impact; snow overlay/random top-rotation appearance and compatibility with the full mod stack are not verified. Guest permissions and remote-player gameplay are untouched. No live game/server files are changed by the build/test/package scripts.
