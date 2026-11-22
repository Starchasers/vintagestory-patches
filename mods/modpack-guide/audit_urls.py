"""Read-only link health. 403/429/timeouts are unknown, never evidence of mod removal."""
import concurrent.futures,json,re,urllib.request,urllib.error,zipfile
from pathlib import Path
W=Path(__file__).parent
z=zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.1.0.zip')
lang=json.loads(z.read('assets/kuboczochmodpackguide/lang/en.json'))
urls=sorted(set(re.findall(r'href=[\"\'](https?://[^\"\']+)',str(lang))))
def check(url):
 try:
  with urllib.request.urlopen(urllib.request.Request(url,headers={'User-Agent':'Mozilla/5.0'},method='GET'),timeout=10) as r:
   r.read(100);return {'url':url,'status':r.status,'final':r.url,'health':'reachable'}
 except urllib.error.HTTPError as e:return {'url':url,'status':e.code,'health':'dead' if e.code in (404,410) else 'unknown/blocked'}
 except Exception as e:return {'url':url,'error':str(e),'health':'unknown/network'}
with concurrent.futures.ThreadPoolExecutor(max_workers=12) as ex:rows=list(ex.map(check,urls))
(W/'url-health.json').write_text(json.dumps({'policy':'HTTP 404/410 only means dead URL, never absent mod; blocked pages and timeouts are unknown.','results':rows},indent=2))
from collections import Counter
print(len(rows),dict(Counter(r['health'] for r in rows)))
