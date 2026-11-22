using System.Reflection;
using Vintagestory.API.Common;
public class InventoryProxy : DispatchProxy
{
    public Dictionary<string,IInventory> Inventories = new();
    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        if (targetMethod?.Name == "get_Inventories") return Inventories;
        throw new NotSupportedException(targetMethod?.Name);
    }
}
