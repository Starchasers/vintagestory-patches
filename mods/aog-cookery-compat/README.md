# Kuboczoch AoG Cookery Compatibility 1.0.0

Adds the Rustic Cookery braising interaction and sound to Art of Growing firepits.
No new block IDs, recipes, replacement DLLs or world migration. Construction-stage
behaviors and all existing firepit properties are preserved.

Baseline: Art of Growing 1.2.2 + Rustic Cookery 1.2.0, Vintage Story 1.22.
Dependency versions are minimum versions, not a guarantee for later releases.

Install: back up the world, stop the game/server, put this ZIP (without extracting)
in Mods on the server and every client, then restart. Do not replace either upstream mod.
Rollback: stop game/server, remove only this ZIP, restart.

Acceptance test on a disposable world/copy:
1. Check startup logs for patch errors and missing behavior classes.
2. Build a new Art of Growing firepit and test an existing completed firepit.
3. Follow Rustic Cookery's normal roasting-dish/braising procedure; verify adding,
   cooking, retrieval and sound, then repeat after saving and reloading.
4. Check ordinary pot cooking, fuel, ignition and vanilla firepits still work.

Validation: ZIP integrity, JSON parsing, exact upstream behavior-name/path parity,
and insertion/preservation checks against the supplied AoG asset passed.
Not tested in the game engine or a live client. This is a compatibility candidate,
not a gameplay-verified release. No upstream binaries or assets are redistributed.
