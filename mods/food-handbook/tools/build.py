"""Build own code, metadata-check upstream, exercise trusted API layout; never start game/mods."""
import os
from pathlib import Path
import subprocess
import sys
ROOT = Path(__file__).resolve().parents[1]
DOTNET = os.environ.get("DOTNET","dotnet")
GAME = Path(os.environ.get("GAME_PATH",str(ROOT.parent/"woodworking-tutorial/vs"))).resolve()
ACA = Path(os.environ.get("ACA_PATH",str(ROOT.parent/"food-handbook-hook-spike/evidence/ACulinaryArtillery_2.0.0-dev.29.zip/ACulinaryArtillery.dll"))).resolve()
def run(args, log=None):
    print("RUN", " ".join(map(str,args)),flush=True)
    result=subprocess.run(list(map(str,args)),cwd=ROOT,text=True,stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
    print(result.stdout,end="",flush=True)
    if log: (ROOT/"docs"/log).write_text(result.stdout)
    if result.returncode: raise SystemExit(result.returncode)
run([DOTNET,"build","KuboczochFoodHandbook.csproj","-c","Release","--nologo",f"-p:GamePath={GAME}",f"-p:ACAPath={ACA}"],"compile.txt")
run([DOTNET,"run","--project","tests/Tests.csproj","-c","Release"],"tdd-green.txt")
run([DOTNET,"run","--project","tests/Layout/Layout.csproj","-c","Release",f"-p:GamePath={GAME}"],"layout-tdd-green.txt")
run([DOTNET,"run","--project","tools/MetadataCheck/MetadataCheck.csproj","-c","Release","--",GAME/"VintagestoryAPI.dll",ACA,ROOT/"bin/Release/net10.0/KuboczochFoodHandbook.dll"],"metadata.txt")
run([sys.executable,"tools/validate.py"],"static-green.txt")
run([sys.executable,"tools/package.py"])
run([sys.executable,"tools/validate.py","dist/KuboczochFoodHandbook-0.3.1.zip","dist/KuboczochFoodHandbook-0.3.1-source.zip"],"archive-validation.txt")
# Include the verified archive-check log in source output and refresh checksums.
run([sys.executable,"tools/package.py"])
run([sys.executable,"tools/validate.py","dist/KuboczochFoodHandbook-0.3.1.zip","dist/KuboczochFoodHandbook-0.3.1-source.zip"])
