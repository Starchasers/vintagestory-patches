"""Build 1.4.0 from the immutable 1.3.1 release; no gameplay assets are modified."""
import json,zipfile,re
from pathlib import Path
from process_content import pages,TOPICS,NS,link
W=Path(__file__).parent;P='assets/kuboczochmodpackguide/'
def normalize_display_text(value):
 return ''.join(part if i%2 else part.replace('&amp;','and') for i,part in enumerate(re.split(r'(<[^>]*>)',value)))
def build():
 with zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.3.1.zip') as z:files={n:z.read(n) for n in z.namelist()}
 lang=json.loads(files[P+'lang/en.json'])
 for code,(title,text) in pages().items():
  lang[code+'-title']=title;lang[code+'-text']=text
  d={'pageCode':NS+code,'title':'kuboczochmodpackguide:'+code+'-title','text':'kuboczochmodpackguide:'+code+'-text','categoryCode':'kuboczochmodpackguide-index'}
  files[P+'config/handbook/'+code+'.json']=(json.dumps(d,indent=2)+'\n').encode()
 # Root remains a mod directory, with every category before authored guides.
 lang['root-text']+='<br><br>'+'<br>'.join(link(NS+'process-index-'+topic,label+' guides') for topic,(label,_) in TOPICS.items())
 for topic,(label,_) in TOPICS.items():
  lang['category-'+topic+'-text']+='<br><br><strong>Our guides</strong><br>'+link(NS+'process-index-'+topic,label+' guides')
 files[P+'lang/en.json']=(json.dumps(lang,ensure_ascii=False,indent=2)+'\n').encode()
 info=json.loads(files['modinfo.json']);info['version']='1.4.0';info['description']='Pszczoly Modpack directory, Toolsmith comparisons and concise cross-mod process guides.'
 files['modinfo.json']=(json.dumps(info,indent=2)+'\n').encode()
 target=W/'dist/KuboczochModpackGuide-1.4.0.zip'
 with zipfile.ZipFile(target,'w',compression=zipfile.ZIP_DEFLATED,compresslevel=9) as z:
  for n,b in sorted(files.items()):
   zi=zipfile.ZipInfo(n,(2026,11,17,0,0,0));zi.compress_type=zipfile.ZIP_DEFLATED;zi.external_attr=0o644<<16;z.writestr(zi,b)
 print(target)
if __name__=='__main__':build()
