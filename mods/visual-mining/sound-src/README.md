# Mining hit sound patch integration

Include **only `MiningSoundPatch.cs`** in the main mod, with its normal Vintage Story API and Harmony references. No dependency on a main-mod type. Namespace: `Kuboczoch.VisualMining`.

```csharp
// Client startup only, after the impact-audio system is available:
bool soundPatchReady = MiningSoundPatch.Install((player, selection, slot) =>
    /* current feature enabled + exact local player + actual breaking selection
       + supported tool/animation + impact sound subsystem ready */ false);

// Gate replacement impact audio on MiningSoundPatch.Installed at playback time.
// Otherwise preserve vanilla audio; LastError describes an installation failure.

// Client shutdown:
MiningSoundPatch.Dispose();
```

`Install(System.Func<IPlayer,BlockSelection,ItemSlot,bool>)` returns success; `Installed` is a static bool property; `Dispose()` is static and repeatable. Exceptions in the predicate retain vanilla sound. The shared original sound call still forwards final break (after-damage resistance <= 0, and NaN) without consulting the predicate. Original timestamps, damage, pop, sound getters, branching, and return stay intact. The existing sound-call player is reused as predicate player because the validated IL explicitly loads `ldarg.1` into that position.

The matcher intentionally refuses unfamiliar IL, duplicate/missing calls, altered resistance branch, non-float local1, any exception regions, and labels directly on the replaced call. It leaves incompatible instructions unchanged and sets Installed false. This is deliberately stricter than relocating labeled callsites. Scope is base Block.OnGettingBroken only, not derived overrides or creative breaks.

## Standalone headless test

`dotnet run --project sound-src/SoundTests.csproj` from the parent directory (or `dotnet run --project SoundTests.csproj` here).

The test uses the supplied **actual game API and actual 0Harmony.dll**, installs/uninstalls the real method patch, checks current patched IL, and calls the production helper through a DispatchProxy IWorldAccessor. It validates positive-hit suppression, exact original argument/return forwarding, zero/negative/NaN break preservation, predicate exception fallback, unknown IL rejection, reinstall, and dispose. A direct transform comparison verifies all other original instructions/branch labels unchanged; labels from separate Harmony IL generators are not comparable.

Limitation: DispatchProxy cannot implement this API's IPlayer because it contains a nonpublic default-interface member (`IsInInteractionRangeOf`). Helper tests therefore use null player, real BlockSelection/ItemSlot, and a real IWorldAccessor proxy. This is not a full game/mining integration run or audible playback test. Test project targets installed net10.0; production source uses ordinary APIs and has no net10-specific dependencies.
