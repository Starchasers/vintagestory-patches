"""Package only explicit original files. No upstream assets/binaries are copied."""
from pathlib import Path
import zipfile
import hashlib
ROOT = Path(__file__).resolve().parents[1]
DIST = ROOT / "dist"
NAME = "KuboczochFoodHandbook-0.3.1"
def archive(path, entries):
    with zipfile.ZipFile(path,"w",zipfile.ZIP_DEFLATED,compresslevel=9) as z:
        for name, source in sorted(entries):
            info=zipfile.ZipInfo(name,(2026,1,1,0,0,0))
            info.compress_type=zipfile.ZIP_DEFLATED
            info.external_attr=0o100644 << 16
            z.writestr(info,source.read_bytes())
    print(path)
DIST.mkdir(exist_ok=True)
runtime=[("modinfo.json",ROOT/"modinfo.json"),("KuboczochFoodHandbook.dll",ROOT/"bin/Release/net10.0/KuboczochFoodHandbook.dll"),("README.md",ROOT/"README.md"),("COVERAGE.md",ROOT/"docs/COVERAGE.md")]
runtime += [(p.relative_to(ROOT).as_posix(),p) for p in (ROOT/"assets").rglob("*.json")]
archive(DIST/(NAME+".zip"),runtime)
source=[]
for folder in ("src","tests","tools","assets","docs"):
    for p in (ROOT/folder).rglob("*"):
        if not p.is_file() or any(part in ("bin","obj","__pycache__") for part in p.relative_to(ROOT).parts): continue
        if p.suffix not in (".cs",".csproj",".py",".md",".json",".txt"): continue
        source.append((p.relative_to(ROOT).as_posix(),p))
source += [(name,ROOT/name) for name in ("KuboczochFoodHandbook.csproj","modinfo.json","README.md")]
archive(DIST/(NAME+"-source.zip"),source)
lines=[hashlib.sha256(p.read_bytes()).hexdigest()+"  "+p.name for p in sorted(DIST.glob("*.zip"))]
(DIST/"SHA256SUMS").write_text("\n".join(lines)+"\n")
