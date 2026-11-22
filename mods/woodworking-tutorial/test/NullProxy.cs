using System.Reflection;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
public class NullProxy : DispatchProxy
{
    protected override object? Invoke(MethodInfo? method, object?[]? args)
    {
        if(method!.Name=="get_World") return Create<IClientWorldAccessor,NullProxy>();
        if(method.Name=="get_ClassRegistry" || method.Name=="CreateInvNetworkUtil") return Create(method.ReturnType,typeof(NullProxy));
        if(method.Name=="GetItem") return new Item{Code=(AssetLocation)args![0]!,ItemId=1};
        if(method.Name=="GetBlock") return new Block{Code=(AssetLocation)args![0]!,BlockId=1};
        return method.ReturnType==typeof(EnumLinebreakBehavior)?EnumLinebreakBehavior.AfterWord:
            method.ReturnType.IsValueType?Activator.CreateInstance(method.ReturnType):null;
    }
}
