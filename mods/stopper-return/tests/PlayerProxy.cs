using System.Reflection;
using System.Reflection.Emit;
using Vintagestory.API.Common;

// VS 1.22 IPlayer has an internal interface member, which DispatchProxy cannot
// implement. This test-only emitter grants access only to the API assembly.
public static class PlayerProxy
{
    private static readonly Type Implementation = Build();
    public static IPlayer Create(IPlayerInventoryManager manager, EntityPlayer? entity = null)
    {
        var player = (IPlayer)Activator.CreateInstance(Implementation)!;
        Implementation.GetField("Manager")!.SetValue(player, manager);
        Implementation.GetField("Entity")!.SetValue(player, entity);
        return player;
    }
    private static Type Build()
    {
        var assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("StopperTestPlayer"), AssemblyBuilderAccess.Run);
        assembly.SetCustomAttribute(new CustomAttributeBuilder(
            typeof(System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute).GetConstructor(new[] { typeof(string) })!,
            new object[] { typeof(IPlayer).Assembly.GetName().Name! }));
        var type = assembly.DefineDynamicModule("Player").DefineType("TestPlayer", TypeAttributes.Public);
        type.AddInterfaceImplementation(typeof(IPlayer));
        var manager = type.DefineField("Manager", typeof(IPlayerInventoryManager), FieldAttributes.Public);
        var entity = type.DefineField("Entity", typeof(EntityPlayer), FieldAttributes.Public);
        foreach (var method in typeof(IPlayer).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            var implementation = type.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Final,
                method.ReturnType, method.GetParameters().Select(p => p.ParameterType).ToArray());
            var il = implementation.GetILGenerator();
            if (method.Name == "get_InventoryManager") { il.Emit(OpCodes.Ldarg_0); il.Emit(OpCodes.Ldfld, manager); }
            else if (method.Name == "get_PlayerUID") { il.Emit(OpCodes.Ldstr, "test"); }
            else if (method.Name == "get_Entity") { il.Emit(OpCodes.Ldarg_0); il.Emit(OpCodes.Ldfld, entity); }
            else if (method.ReturnType != typeof(void))
            {
                var value = il.DeclareLocal(method.ReturnType);
                il.Emit(OpCodes.Ldloca, value); il.Emit(OpCodes.Initobj, method.ReturnType); il.Emit(OpCodes.Ldloc, value);
            }
            il.Emit(OpCodes.Ret);
            type.DefineMethodOverride(implementation, method);
        }
        return type.CreateType()!;
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class IgnoresAccessChecksToAttribute(string assemblyName) : Attribute
    { public string AssemblyName { get; } = assemblyName; }
}
