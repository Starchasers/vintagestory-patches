using System.Text.Json;
using System.Text.Json.Serialization;
namespace SaferSurface;
/// <summary>Restart-only strict config. Never overwrite an existing file, including null/empty/malformed JSON.</summary>
public static class ConfigFile {
 public static SurfaceConfig LoadOrCreate(string path){
  var options=new JsonSerializerOptions{WriteIndented=true,PropertyNameCaseInsensitive=true,UnmappedMemberHandling=JsonUnmappedMemberHandling.Disallow};
  SurfaceConfig config;
  if(File.Exists(path)){
   if(new FileInfo(path).Length>1048576)throw new InvalidDataException("Config exceeds 1 MiB");
   string text=File.ReadAllText(path);
   using var document=JsonDocument.Parse(text);
   if(document.RootElement.ValueKind!=JsonValueKind.Object)throw new InvalidDataException("Config root must be an object");
   var names=new HashSet<string>(StringComparer.OrdinalIgnoreCase);
   foreach(var property in document.RootElement.EnumerateObject())if(!names.Add(property.Name))throw new InvalidDataException("Duplicate config property: "+property.Name);
   config=JsonSerializer.Deserialize<SurfaceConfig>(text,options)??throw new InvalidDataException("Null config");
  }else{
   config=new SurfaceConfig();
   using var output=new FileStream(path,FileMode.CreateNew,FileAccess.Write);
   JsonSerializer.Serialize(output,config,options);
  }
  var error=config.Validate();if(error!=null)throw new InvalidDataException(error);
  return config;
 }
}
