"""Private authorized asset extraction; input is NOT executed. No gameplay patches copied."""
from pathlib import Path
import json,zipfile,hashlib,copy,sys
ROOT=Path(__file__).resolve().parent
ORIGINAL=Path(sys.argv[1]) if len(sys.argv)>1 else Path('/home/brunette/.hermes/cache/scratch/immersive-mining/mods/ImmersiveMining_0.3.0.zip')
DEST=ROOT/'package'
DEST.mkdir(exist_ok=True)
MAPPING={'pickaxenew':'kvm-pickaxe','axechopnew':'kvm-axe','axenew':'kvm-axe','shovelnew':'kvm-shovel','shoveldignew':'kvm-shovel','axechophorizontalnew':'kvm-axe-horizontal'}
reports=[]
with zipfile.ZipFile(ORIGINAL) as z:
 for filename in ['pickaxenew','axenew','shovelnew','axechophorizontalnew']:
  old=f'assets/immersivemining/patches/animations/{filename}.json'
  patches=json.loads(z.read(old))
  for p in patches:
   assert p['op'].lower()=='add'
   assert p['path'] in ['/client/animations/-','/animations/-']
   p['side']='Client'
   v=p['value']; oldcode=v['code']; newcode=MAPPING[oldcode];v['code']=newcode
   if 'animation' in v:
    v['animation']=newcode
    v.pop('attributes',None) # no damageAtFrame or callback sounds
   else:
    v['name']=newcode
   reports.append({'asset':old,'oldcode':oldcode,'newcode':newcode,'path':p['path'],'frames':v.get('quantityframes')})
  onehand=copy.deepcopy(patches)
  left={'UpperArmL','LowerArmL','ItemAnchorL','LeftHand','HandL'}
  for p in onehand:
   v=p['value'];v['code']+='-onehand'
   if 'animation' in v:
    v['animation']+='-onehand'
    for name in left:v.setdefault('elementWeight',{})[name]=0
   else:
    v['name']+='-onehand'
    for frame in v.get('keyframes',[]):
     for name in left:frame.get('elements',{}).pop(name,None)
  patches+=onehand
  out=DEST/f'assets/kuboczochvisualmining/patches/animations/{filename}.json'
  out.parent.mkdir(parents=True,exist_ok=True);out.write_text(json.dumps(patches,indent=2)+'\n')
 # Resolve the actual block's current GetSounds at contact; no global sound asset overrides.
 (DEST/'modicon.png').write_bytes(z.read('modicon.png'))
(DEST/'modinfo.json').write_text(json.dumps({'type':'code','modid':'kuboczochvisualmining','name':'Kuboczoch Visual Mining','authors':['Kuboczoch'],'description':'Private standalone client-side cosmetic mining animations and voxel chips. Original visual assets by SaltyWater and GamingToast. No gameplay changes.','version':'1.0.0','side':'Client','requiredOnClient':False,'requiredOnServer':False,'dependencies':{'game':'1.22.7'}},indent=2)+'\n')
(ROOT/'asset-audit.json').write_text(json.dumps({'source_sha256':hashlib.sha256(ORIGINAL.read_bytes()).hexdigest(),'patches':reports,'excluded':'All original DLL, tool/item/gameplay patches, knife animations, networking and config'},indent=2)+'\n')
print('Prepared',len(reports),'animation patch operations; metadata and shape only.')
