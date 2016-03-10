using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace AppSettingsByConventionTests.ProxyBuilding
{
    internal static class InterfaceImplementationExtensions
    {
        private const TypeAttributes PublicClassAttributes = TypeAttributes.Class | TypeAttributes.NotPublic;

        public static Type ImplementClassWithProperties(this Type @interface)
        {
            if (!@interface.IsInterface)
            {
                throw new InvalidOperationException("Not an interface: " + @interface.FullName);
            }

            var typeBuilder = GetPrivateTypeBuilder(@interface.Name.Substring(1));
            foreach (var propertyInfo in @interface.GetProperties())
            {
                typeBuilder.AddPropertyWithBackingField(propertyInfo);
            }
            typeBuilder.AddInterfaceImplementation(@interface);
            return typeBuilder.CreateType();
        }
        
        private static TypeBuilder GetPrivateTypeBuilder(string typeName)
        {
            var assemblyName = new AssemblyName { Name = "tmpAssembly" };
            var assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("tmpModule");
            var typeBuilder = moduleBuilder.DefineType(typeName, PublicClassAttributes);
            return typeBuilder;
        }
    }
}