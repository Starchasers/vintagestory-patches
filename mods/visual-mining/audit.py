"""Archive/source invariants; run from this directory. Does not load original code."""
from pathlib import Path
import json,re
root=Path(__file__).resolve().parent
package=root/'package'
info=json.loads((package/'modinfo.json').read_text())
assert info['modid']=='kuboczochvisualmining' and info['authors']==['Kuboczoch']
assert info['side']=='Client' and info['requiredOnServer'] is False and info['requiredOnClient'] is False
assert info['dependencies']=={'game':'1.22.7'}
assets=list((package/'assets').rglob('*'))
assert all('/assets/kuboczochvisualmining/' in str(p) for p in assets if p.is_file())
ops=[]; metadata={}; shapes={}
for path in (package/'assets').rglob('*.json'):
    assert '/patches/animations/' in str(path)
    for op in json.loads(path.read_text()):
        assert op['op']=='Add' and op['side']=='Client'
        assert op['file'] in ['game:entities/humanoid/player','game:shapes/entity/humanoid/seraph-faceless']
        assert op['path'] in ['/client/animations/-','/animations/-']
        v=op['value'];assert v['code'].startswith('kvm-')
        if op['path']=='/client/animations/-':
            assert 'attributes' not in v and v['animation']==v['code']
            metadata[v['code']]=v
        else:shapes[v['code']]=v
        ops.append(op)
assert len(ops)==16 and len(metadata)==len(shapes)==8 and set(metadata)==set(shapes)
for name,v in shapes.items():
    assert v['quantityframes']==(52 if 'pickaxe' in name else 40)
    if name.endswith('-onehand'):
        for k in v['keyframes']:assert not {'UpperArmL','LowerArmL','ItemAnchorL','LeftHand','HandL'} & set(k['elements'])
production=[root/'src/LocalAnimationAssets.cs',root/'src/VisualMiningMod.cs',root/'voxel-src/CosmeticVoxelRenderer.cs',root/'voxel-src/VoxelDamageState.cs',root/'sound-src/MiningSoundPatch.cs']
for p in production:
    text=p.read_text()
    for forbidden in [r'\.SetBlock\s*\(',r'\.ExchangeBlock\s*\(',r'\.RegisterChannel\s*\(',r'\.DamageItem\s*\(',r'\.RemainingResistance\s*[-+*/]?=',r'\.Controls\.HandUse\s*=',r'\.SetBlockEntity\s*\(']:
        assert not re.search(forbidden,text), (str(p),forbidden)
assert not any('ImmersiveMining.dll'==p.name or p.name.startswith('Vintagestory') for p in package.rglob('*.dll'))
print('PASS metadata: independent client-only modid, author, game dependency, no IM dependency')
print('PASS assets: 16 additive animation-only operations, 8 distinct registered clips, no gameplay patches or game-domain overrides')
print('PASS offhand: all 4 variant clips exclude left-arm keyframes')
print('PASS production audit: no block writes, networking, durability mutation, resistance mutation or hand-use assignments')
print('NOTE static invariants plus headless tests are not a rendered in-game acceptance test')
