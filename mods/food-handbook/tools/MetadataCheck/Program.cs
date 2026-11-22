// Read PE metadata only: never Assembly.Load or execute upstream assemblies.
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Collections.Immutable;
using System.Security.Cryptography;
if (args.Length!=3) throw new Exception("Expected API.dll ACA.dll addon.dll");
MetadataReader Open(string path, out PEReader pe) { pe=new PEReader(File.OpenRead(path)); return pe.GetMetadataReader(); }
void Require(bool ok,string text) { if (!ok) throw new Exception(text); Console.WriteLine("PASS " + text); }
var api=Open(args[0],out var apiPe);
using(apiPe) {
 var type=api.TypeDefinitions.Select(api.GetTypeDefinition).Single(t=>api.GetString(t.Name)=="GameVersion");
 var field=type.GetFields().Select(api.GetFieldDefinition).Single(f=>api.GetString(f.Name)=="OverallVersion");
 var value=api.GetBlobReader(api.GetConstant(field.GetDefaultValue()).Value).ReadUTF16(api.GetBlobBytes(api.GetConstant(field.GetDefaultValue()).Value).Length);
 Require(value=="1.22.7","trusted game reference OverallVersion = 1.22.7");
}
Require(Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(args[1])))=="8F6CB3C873F39CC2E3B898AA538015370608547B6F4D34A0FBDAE51EC255406B","exact trusted ACA assembly semantics including helper lambdas");
var aca=Open(args[1],out var acaPe);
using(acaPe) {
 var type=aca.TypeDefinitions.Select(aca.GetTypeDefinition).Single(t=>aca.GetString(t.Name)=="HandbookInfoExtensions" && aca.GetString(t.Namespace)=="ACulinaryArtillery.Util");
 var method=type.GetMethods().Select(aca.GetMethodDefinition).Single(m=>aca.GetString(m.Name)=="ACAHandbookIngredientForComponents");
 Require(method.Attributes.HasFlag(MethodAttributes.Public) && method.Attributes.HasFlag(MethodAttributes.Static),"ACA hook is public static");
 var sig=method.DecodeSignature(new Names(),(object?)null);
 Require(sig.ReturnType=="System.Collections.Generic.List`1<Vintagestory.API.Client.RichTextComponentBase>","ACA helper returns richtext list");
 Require(sig.ParameterTypes.SequenceEqual(new[]{"Vintagestory.API.Client.ICoreClientAPI","Vintagestory.API.Common.ItemStack[]","Vintagestory.API.Common.ActionConsumable`1<System.String>","Vintagestory.API.Common.ItemStack"}),"exact ACA hook parameter signature");
 Require(method.GetParameters().Select(aca.GetParameter).Where(p=>p.SequenceNumber>0).Select(p=>aca.GetString(p.Name)).SequenceEqual(new[]{"capi","allStacks","openDetailPageFor","stack"}),"Harmony name binding matches exact ACA parameters");
}
// Decode actual target IL without loading the ACA assembly.
using(var targetPe=new PEReader(File.OpenRead(args[1]))) {
 var r=targetPe.GetMetadataReader();
 var type=r.TypeDefinitions.Select(r.GetTypeDefinition).Single(t=>r.GetString(t.Name)=="GetHandbookIngredientForPatch" && r.GetString(t.Namespace)=="ACulinaryArtillery");
 var target=type.GetMethods().Select(r.GetMethodDefinition).Single(m=>r.GetString(m.Name)=="Postfix");
 var parameters=target.DecodeSignature(new Names(),(object?)null).ParameterTypes;
 Require(target.Attributes.HasFlag(MethodAttributes.Static) && parameters.SequenceEqual(new[]{
  "System.Boolean&","Vintagestory.API.Client.ICoreClientAPI","Vintagestory.API.Common.ItemStack[]",
  "Vintagestory.API.Common.ActionConsumable`1<System.String>","Vintagestory.API.Common.ItemStack",
  "System.Collections.Generic.List`1<Vintagestory.API.Client.RichTextComponentBase>","System.Single",
  "System.Collections.Generic.List`1<Vintagestory.API.Common.ItemStack>","System.Collections.Generic.List`1<Vintagestory.API.Common.ItemStack>",
  "System.Collections.Generic.List`1<Vintagestory.API.Common.ItemStack>","System.Boolean"}),"exact ACA Postfix positional ABI: ref result arg0, components arg5");
 var ops=typeof(OpCodes).GetFields().Where(f=>f.FieldType==typeof(OpCode)).Select(f=>(OpCode)f.GetValue(null)!).ToDictionary(o=>unchecked((ushort)o.Value));
 var il=targetPe.GetMethodBody(target.RelativeVirtualAddress).GetILBytes()!; int calls=0;
 for(int p=0;p<il.Length;) {
  ushort key=il[p++]; if(key==0xfe) key=(ushort)(0xfe00|il[p++]); var op=ops[key];
  int size=op.OperandType switch {
   OperandType.InlineNone=>0,OperandType.ShortInlineBrTarget or OperandType.ShortInlineI or OperandType.ShortInlineVar=>1,
   OperandType.InlineVar=>2,OperandType.InlineI8 or OperandType.InlineR=>8,
   OperandType.InlineSwitch=>4+4*BitConverter.ToInt32(il,p),_=>4};
  if(op==OpCodes.Call) {
   var h=MetadataTokens.EntityHandle(BitConverter.ToInt32(il,p));
   if(h.Kind==HandleKind.MethodDefinition && r.GetString(r.GetMethodDefinition((MethodDefinitionHandle)h).Name)=="ACAHandbookIngredientForComponents") calls++;
  }
  p+=size;
 }
 Require(calls==1,"actual ACA Postfix IL contains exactly one direct ingredient-helper call");
}
using(var nativePe=new PEReader(File.OpenRead(Path.Combine(Path.GetDirectoryName(args[0])!,"Mods/VSSurvivalMod.dll")))) {
 var r=nativePe.GetMetadataReader();
 var t=r.TypeDefinitions.Select(r.GetTypeDefinition).Single(t=>r.GetString(t.Name)=="CollectibleBehaviorHandbookTextAndExtraInfo");
 var heading=t.GetMethods().Select(r.GetMethodDefinition).Single(m=>r.GetString(m.Name)=="AddHeading");
 Require(heading.DecodeSignature(new Names(),(object?)null).ParameterTypes.Length==4,"exact native AddHeading has FOUR parameters; preliminary optional-link API claim corrected");
 var subheading=t.GetMethods().Select(r.GetMethodDefinition).Single(m=>r.GetString(m.Name)=="AddSubHeading");
 Require(subheading.DecodeSignature(new Names(),(object?)null).ParameterTypes.Length==5,"native linked subheading signature retained");
 var m=t.GetMethods().Select(r.GetMethodDefinition).Single(m=>r.GetString(m.Name)=="addIngredientForInfo");
 var body=nativePe.GetMethodBody(m.RelativeVirtualAddress);
 var general=t.GetMethods().Select(r.GetMethodDefinition).Single(m=>r.GetString(m.Name)=="addProcessesIntoInfo");
 Console.WriteLine("GENERAL_IL="+Convert.ToHexString(SHA256.HashData(nativePe.GetMethodBody(general.RelativeVirtualAddress).GetILBytes()!)));

 Require(general.GetParameters().Select(p=>r.GetString(r.GetParameter(p).Name)).SequenceEqual(new[]{"capi","openDetailPageFor","stack","components","marginTop","marginBottom","containers","fuels","haveText"}),"native Processes into ABI, no alternate renderer or added hook");
 Require(Convert.ToHexString(SHA256.HashData(nativePe.GetMethodBody(general.RelativeVirtualAddress).GetILBytes()!))=="08D01932676FDA0BD552D689FF84150666ECAA6317B6FFFCD03AE8FC4B535101","exact existing native Processes into IL fingerprint");
 Console.WriteLine("GENERAL_ABI="+string.Join(",",general.GetParameters().Select(p=>r.GetString(r.GetParameter(p).Name))));
 Console.WriteLine("NATIVE_IL="+Convert.ToHexString(SHA256.HashData(body.GetILBytes()!)));
 Require(Convert.ToHexString(SHA256.HashData(body.GetILBytes()!))=="E2ADD5DAADB951752CBFE9280897F4435734707CF612BEE8CFCD7BA053A46207","exact native ingredient IL fingerprint");
 var nativeOps=typeof(OpCodes).GetFields(BindingFlags.Public|BindingFlags.Static).Where(f=>f.FieldType==typeof(OpCode)).Select(f=>(OpCode)f.GetValue(null)!).ToDictionary(o=>o.Value);
 int CountCalls(byte[] bytes,int targetToken) {
  int found=0;
  for(int pos=0;pos<bytes.Length;) {
   short key=bytes[pos++];if(key==0xfe)key=(short)(0xfe00|bytes[pos++]);var op=nativeOps[key];
   if(targetToken==0 && op==OpCodes.Ldstr && r.GetUserString(MetadataTokens.UserStringHandle(BitConverter.ToInt32(bytes,pos)&0x00ffffff))=="handbook-processesinto-transition-")found++;
   if(op.OperandType==OperandType.InlineMethod && BitConverter.ToInt32(bytes,pos)==targetToken && (op==OpCodes.Call || op==OpCodes.Callvirt))found++;
   pos+=op.OperandType switch {OperandType.InlineNone=>0,OperandType.ShortInlineBrTarget or OperandType.ShortInlineI or OperandType.ShortInlineVar=>1,OperandType.InlineVar=>2,OperandType.InlineI8 or OperandType.InlineR=>8,OperandType.InlineSwitch=>4+4*BitConverter.ToInt32(bytes,pos),_=>4};
  }
  return found;
 }
 Console.WriteLine("GENERAL_KEYS="+CountCalls(nativePe.GetMethodBody(general.RelativeVirtualAddress).GetILBytes()!,0));
 Require(CountCalls(nativePe.GetMethodBody(general.RelativeVirtualAddress).GetILBytes()!,0)==1,"native transition heading is already named per EnumTransitionType, not generic text");
 var collector=t.GetMethods().Single(h=>r.GetString(r.GetMethodDefinition(h).Name)=="addToListUniquely");
 Require(CountCalls(body.GetILBytes()!,MetadataTokens.GetToken(collector))==12,"exact 12 native collector calls; source-locked ordinals grid1/barrel6, returnedStack2 untouched");
 var localNames=r.GetStandaloneSignature(body.LocalSignature).DecodeLocalSignature(new Names(),(object?)null);
 Require(localNames.Count(n=>n=="Vintagestory.API.Common.GridRecipe")==1 && localNames.Count(n=>n=="Vintagestory.GameContent.BarrelRecipe")==1,"unambiguous grid/barrel recipe locals for contribution capture");
 var add=t.GetMethods().Select(r.GetMethodDefinition).Single(m=>r.GetString(m.Name)=="addToListUniquely");
 var sig=add.DecodeSignature(new Names(),(object?)null);
 Require(sig.ReturnType=="System.Void" && sig.ParameterTypes.SequenceEqual(new[]{"Vintagestory.API.Client.ICoreClientAPI","System.Collections.Generic.List`1<Vintagestory.API.Common.ItemStack>","Vintagestory.API.Common.ItemStack"}),"native output collector ABI: instance void (capi,list,output)");
 Console.WriteLine("NATIVE_ABI="+string.Join(",",m.DecodeSignature(new Names(),(object?)null).ParameterTypes));
}
var own=Open(args[2],out var ownPe);
using(ownPe) {
 var types=own.TypeDefinitions.Select(own.GetTypeDefinition).ToArray();
 Require(own.GetAssemblyDefinition().Version==new Version(0,3,1,0),"production assembly version 0.3.1.0");
 Require(types.Any(t=>own.GetString(t.Name)=="SectionCompositionPatch"),"production contains independent section composition hook");
 Require(types.Any(t=>own.GetString(t.Name)=="FoodHandbookModSystem" && own.GetString(t.Namespace)=="Kuboczoch.FoodHandbook"),"addon contains ModSystem");
 Require(types.All(t=>own.GetString(t.Namespace).StartsWith("Kuboczoch.FoodHandbook") || own.GetString(t.Namespace)==""),"no bundled upstream namespaces");
 Require(own.AssemblyReferences.Select(own.GetAssemblyReference).Any(a=>own.GetString(a.Name)=="ACulinaryArtillery"),"ACA retained as metadata reference not bundled code");
}
foreach(var path in args) Console.WriteLine(Path.GetFileName(path)+" sha256="+Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path))).ToLowerInvariant());
Console.WriteLine("Metadata checks only; no upstream DLL or game client executed.");
sealed class Names : ISignatureTypeProvider<string,object?> {
 public string GetArrayType(string e,ArrayShape s)=>e+"["+new string(',',s.Rank-1)+"]";
 public string GetByReferenceType(string e)=>e+"&";
 public string GetFunctionPointerType(MethodSignature<string> s)=>"fn";
 public string GetGenericInstantiation(string g,ImmutableArray<string> a)=>g+"<"+string.Join(",",a)+">";
 public string GetGenericMethodParameter(object? c,int i)=>"!!"+i;
 public string GetGenericTypeParameter(object? c,int i)=>"!"+i;
 public string GetModifiedType(string m,string u,bool r)=>u;
 public string GetPinnedType(string e)=>e;
 public string GetPointerType(string e)=>e+"*";
 public string GetPrimitiveType(PrimitiveTypeCode c)=>"System."+c;
 public string GetSZArrayType(string e)=>e+"[]";
 public string GetTypeFromDefinition(MetadataReader r,TypeDefinitionHandle h,byte k) { var t=r.GetTypeDefinition(h); return r.GetString(t.Namespace)+"."+r.GetString(t.Name); }
 public string GetTypeFromReference(MetadataReader r,TypeReferenceHandle h,byte k) { var t=r.GetTypeReference(h); return r.GetString(t.Namespace)+"."+r.GetString(t.Name); }
 public string GetTypeFromSpecification(MetadataReader r,object? c,TypeSpecificationHandle h,byte k)=>r.GetTypeSpecification(h).DecodeSignature(this,c);
}
