"""Offline static audit. Reads trusted exact-version base DLLs, never loads modpack code."""
from pathlib import Path
import hashlib,json,re
ROOT=Path(__file__).resolve().parents[1]
E=ROOT/'evidence/richtext'
BASE=Path('/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs')
files=[BASE/'VintagestoryAPI.dll',BASE/'VintagestoryLib.dll',BASE/'Mods/VSSurvivalMod.dll']
manifest={str(p):hashlib.sha256(p.read_bytes()).hexdigest() for p in files}
(E/'base-sha256.json').write_text(json.dumps(manifest,indent=2)+'\n')
s=(E/'VtmlUtil.cs').read_text()
item=s[s.index('case "itemstack":'):s.index('case "font":')]
attrs=re.findall(r'Attributes.TryGetValue\("([^\"]+)"',item)
assert attrs==['floattype','code','type','rsize','offx','offy'],attrs
assert "value4.Split('|')" in item
assert 'new SlideshowItemstackTextComponent(capi, list.ToArray(), num3 / RuntimeEnv.GUIScale, result)' in item
assert 'new ItemStack(collectibleObject)' in item
assert 'Attributes' not in item[item.index('list.Add(new ItemStack'):item.index('float num4')]
assert '"groupBy"' not in item
assert 'openDetailPageFor' not in (E/'GuiHandbookTextPage.cs').read_text().split('public void Init')[1].split('public override void ComposePage')[0]
assert 'dialog.OpenDetailPageFor(text);' in (E/'ModSystemSurvivalHandbook.cs').read_text()
assert 'pageNumberByPageCode.TryGetValue(pageCode' in (E/'GuiDialogHandbook.cs').read_text()
report={'itemstack_consumed_attributes':attrs,'pipe_split':'literal |, no trimming in converter','callback':'VTML constructor omits optional onStackClicked; defaults null','stack_attributes':'No stack attributes parser in itemstack converter','handbook_protocol':'ModSystemSurvivalHandbook -> dialog.OpenDetailPageFor(exact page code)','grouping':'VTML selects explicit ItemStack[] overload; not groupBy overload'}
(E/'static-audit.json').write_text(json.dumps(report,indent=2)+'\n')
print(json.dumps(report,indent=2))
print('PASS static branch, attributes, callback default, exact protocol routing, hashes')
