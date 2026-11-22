"""Run from this directory. Static decompilation ONLY; never loads Toolsmith as code."""
import hashlib
import json
import os
from pathlib import Path
import subprocess
import zipfile

ROOT = Path(__file__).resolve().parent
os.chdir(ROOT)
TOOLCHAIN = Path('/home/brunette/.hermes/cache/scratch/woodworking-tutorial')
ARCHIVE = Path('/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/toolsmith_1.2.19.zip')
OUT = ROOT / 'evidence/attributes'
OUT.mkdir(parents=True, exist_ok=True)
with zipfile.ZipFile(ARCHIVE) as z:
    (OUT / 'Toolsmith.dll').write_bytes(z.read('Toolsmith.dll'))
API = TOOLCHAIN / 'vs/VintagestoryAPI.dll'
CLASSES = {
    OUT / 'Toolsmith.dll': ['Toolsmith.Client.Behaviors.ModularPartRenderingFromAttributes',
        'Toolsmith.ToolsmithModSystem', 'Toolsmith.Client.PartData',
        'Toolsmith.ToolTinkering.Behaviors.CollectibleBehaviorToolHandle',
        'Toolsmith.Utils.ToolsmithAttributes', 'Toolsmith.Client.MultiPartRenderingHelpers',
        'Toolsmith.RecipeRegisterModSystem', 'Toolsmith.ToolTinkering.Items.ItemTinkerToolParts',
        'Toolsmith.ToolTinkering.Items.ItemWhetstone'],
    API: ['Vintagestory.API.Datastructures.TreeAttribute',
        'Vintagestory.API.Common.CollectibleObject', 'Vintagestory.API.Config.GlobalConstants']}
env = dict(os.environ, DOTNET_ROOT=str(TOOLCHAIN / 'dotnet'))
manifest = {'mode': 'ilspy static decompilation, no target DLL execution',
            'archive': str(ARCHIVE), 'archiveSha256': hashlib.sha256(ARCHIVE.read_bytes()).hexdigest(),
            'assemblies': {str(p): hashlib.sha256(p.read_bytes()).hexdigest() for p in CLASSES}, 'commands': []}
for assembly, types in CLASSES.items():
    for typename in types:
        cmd = [str(TOOLCHAIN / 'tools/ilspycmd'), '-t', typename, str(assembly)]
        output = subprocess.run(cmd, env=env, check=True, capture_output=True, text=True).stdout
        (OUT / (typename.rsplit('.', 1)[-1] + '.cs')).write_text(output)
        manifest['commands'].append(cmd)
(OUT / 'decompile_manifest.json').write_text(json.dumps(manifest, indent=2) + '\n')
subprocess.run(['python', 'research_inventory.py'], check=True)
subprocess.run(['python', 'verify_attribute_inventory.py'], check=True)
