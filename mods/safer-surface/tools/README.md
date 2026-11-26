# Build and package

Run these commands from `mods/safer-surface` in the `Starchasers/vintagestory-patches` repository. CI is defined at the repository root in `.github/workflows/safer-surface.yml`.

Requirements: Python 3.10+ (standard library only), .NET SDK 10, and a separately
licensed Vintage Story installation/SDK matching the target server. Do not copy
or commit game DLLs. The SDK-free policy and packaging tests need no game files:

```sh
dotnet run --project tests/PolicyTests.csproj --configuration Release
python3 -m unittest discover -s tests -p 'test_*.py' -v
```

Build the production `src/SaferSurface.csproj` targeting `net10.0`:

```sh
python3 tools/package.py --game-dir /opt/vintagestory
# Optional executable selection:
python3 tools/package.py --game-dir /opt/vintagestory --dotnet /opt/dotnet/dotnet
(cd dist && sha256sum --check kuboczochssafersurface-0.1.0.zip.sha256)
```

The script always invokes a Release build with explicit `-p:GameDir`, deterministic
and CI-build properties into a clean temporary output directory. It packages only
these root entries into `dist/kuboczochssafersurface-0.1.0.zip`:

- `KuboczochsSaferSurface.dll`
- `modinfo.json`
- `README.md`
- `LICENSE`

Metadata must identify `kuboczochssafersurface` version `0.1.0`, `type: code`,
`side: Server`, `requiredOnClient: false`, and `requiredOnServer: true`.
The script rejects symlink inputs/destinations, non-allowlisted/traversing names,
duplicate metadata keys, empty files, disguised executables in text files, and
focused private-key/token/home-path patterns. It never recursively copies build
outputs; SDK/provider DLLs, PDBs and local configuration are not shipped.

Entries are sorted, timestamped 1980-01-01, and given fixed Unix file modes.
After writing, each entry is read back and checked against its original bytes and
CRC before replacement. The sidecar `.zip.sha256` uses standard `sha256sum` format.
Byte identity assumes identical build inputs, SDK/toolchain (including Python and
zlib), and owned files. A checksum proves integrity, not authorship or native
compatibility. The PE-signature check is not a managed-assembly verifier.

Python tests use conspicuously synthetic bytes and temporary directories only.
Their successful deterministic repeats are **not a release build**, SDK integration
test, or native game smoke test. There is intentionally no CLI `--skip-build` or
assembly override. Failed builds do not replace an existing ZIP; an old artifact
may remain in `dist`, so require command success before using an output.

## Optional CI SDK integration

Ordinary push/PR CI runs SDK-free tests only. For a trusted manual dispatch, provision
a self-hosted Linux runner labeled `vintage-story-sdk`, install .NET 10 and Python
3.10+, and set repository variable `VS_GAME_DIR` to its licensed SDK directory.
Select `sdk_integration` when dispatching. No proprietary assemblies are fetched,
installed or uploaded by this workflow; only the four-file owned ZIP and checksum
are uploaded. Never run untrusted branches on the SDK runner. This build job does
not constitute a live-server gameplay test.
