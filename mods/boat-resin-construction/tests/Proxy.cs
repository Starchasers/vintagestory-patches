using System.Reflection;
using System.Reflection.Emit;

// Headless interface fixtures only. Game interfaces include internal methods that
// DispatchProxy cannot implement; emit explicit implementations with test-only access.
public static class NativeProxy
{
    private static readonly AssemblyBuilder Assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("GameTestProxies"), AssemblyBuilderAccess.Run);
    private static readonly ModuleBuilder Module = Assembly.DefineDynamicModule("Proxies");
    private static int serial;
    static NativeProxy()
    {
        var attribute = typeof(System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute).GetConstructor(new[] {typeof(string)})!;
        Assembly.SetCustomAttribute(new CustomAttributeBuilder(attribute, new object[] {"VintagestoryAPI"}));
    }
    public static T Make<T>(System.Func<MethodInfo, object?[]?, object?> handler) where T : class
    {
        var t = Module.DefineType("Proxy" + ++serial, TypeAttributes.Public, typeof(object), new[] {typeof(T)});
        var field = t.DefineField("Handler", handler.GetType(), FieldAttributes.Public);
        var interfaces = typeof(T).GetInterfaces().Append(typeof(T));
        var index = 0;
        foreach (var contract in interfaces)
        foreach (var method in contract.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        {
            var parameters = method.GetParameters();
            var types = parameters.Select(p => p.ParameterType).ToArray();
            var impl = t.DefineMethod("Method" + ++index, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.NewSlot);
            if (method.IsGenericMethodDefinition)
            {
                var arguments = method.GetGenericArguments();
                var generated = impl.DefineGenericParameters(arguments.Select(a => a.Name).ToArray());
                Type Substitute(Type type) => type.IsGenericParameter ? generated[type.GenericParameterPosition]
                    : type.IsByRef ? Substitute(type.GetElementType()!).MakeByRefType()
                    : type.IsArray ? Substitute(type.GetElementType()!).MakeArrayType()
                    : type.IsGenericType ? type.GetGenericTypeDefinition().MakeGenericType(type.GetGenericArguments().Select(Substitute).ToArray()) : type;
                for (int i=0;i<arguments.Length;i++)
                {
                    generated[i].SetGenericParameterAttributes(arguments[i].GenericParameterAttributes);
                    var constraints = arguments[i].GetGenericParameterConstraints().Select(Substitute).ToArray();
                    var baseType = constraints.FirstOrDefault(c => !c.IsInterface);
                    if (baseType != null) generated[i].SetBaseTypeConstraint(baseType);
                    generated[i].SetInterfaceConstraints(constraints.Where(c => c.IsInterface).ToArray());
                }
                impl.SetReturnType(Substitute(method.ReturnType));
                impl.SetParameters(types.Select(Substitute).ToArray());
                var stub = impl.GetILGenerator();
                stub.Emit(OpCodes.Newobj, typeof(NotSupportedException).GetConstructor(Type.EmptyTypes)!); stub.Emit(OpCodes.Throw);
                t.DefineMethodOverride(impl, method);
                continue;
            }
            impl.SetReturnType(method.ReturnType);
            impl.SetParameters(types);
            var il = impl.GetILGenerator();
            if (types.Any(p => p.IsByRef || p.IsPointer) || method.ReturnType.IsByRef)
            {
                il.Emit(OpCodes.Newobj, typeof(NotSupportedException).GetConstructor(Type.EmptyTypes)!);
                il.Emit(OpCodes.Throw);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_0); il.Emit(OpCodes.Ldfld, field);
                il.Emit(OpCodes.Ldtoken, method); il.Emit(OpCodes.Ldtoken, contract);
                il.Emit(OpCodes.Call, typeof(MethodBase).GetMethod(nameof(MethodBase.GetMethodFromHandle), new[] {typeof(RuntimeMethodHandle), typeof(RuntimeTypeHandle)})!);
                il.Emit(OpCodes.Castclass, typeof(MethodInfo));
                il.Emit(OpCodes.Ldc_I4, types.Length); il.Emit(OpCodes.Newarr, typeof(object));
                for (int i = 0; i < types.Length; i++)
                {
                    il.Emit(OpCodes.Dup); il.Emit(OpCodes.Ldc_I4, i); il.Emit(OpCodes.Ldarg, i+1);
                    if (types[i].IsValueType) il.Emit(OpCodes.Box, types[i]);
                    il.Emit(OpCodes.Stelem_Ref);
                }
                il.Emit(OpCodes.Callvirt, handler.GetType().GetMethod("Invoke")!);
                if (method.ReturnType == typeof(void)) il.Emit(OpCodes.Pop);
                else if (method.ReturnType.IsValueType) il.Emit(OpCodes.Unbox_Any, method.ReturnType);
                else il.Emit(OpCodes.Castclass, method.ReturnType);
                il.Emit(OpCodes.Ret);
            }
            t.DefineMethodOverride(impl, method);
        }
        var instance = Activator.CreateInstance(t.CreateType()!)!;
        var safe = new System.Func<MethodInfo,object?[]?,object?>((m,a) => handler(m,a) ?? (m.ReturnType != typeof(void) && m.ReturnType.IsValueType ? Activator.CreateInstance(m.ReturnType) : null));
        instance.GetType().GetField("Handler")!.SetValue(instance, safe);
        return (T)instance;
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class IgnoresAccessChecksToAttribute(string assemblyName) : Attribute
    {
        public string AssemblyName { get; } = assemblyName;
    }
}
