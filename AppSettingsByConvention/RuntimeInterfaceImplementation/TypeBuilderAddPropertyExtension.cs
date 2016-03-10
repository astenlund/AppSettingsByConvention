using System;
using System.Reflection;
using System.Reflection.Emit;

namespace AppSettingsByConvention.RuntimeInterfaceImplementation
{
    internal static class TypeBuilderAddPropertyExtension
    {
        private const MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual;

        public static void AddPropertyWithBackingField(this TypeBuilder typeBuilder, PropertyInfo propertyInfo)
        {
            var propertyName = propertyInfo.Name;
            var propertyType = propertyInfo.PropertyType;
            var privateFieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);
            var propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.None, propertyType,
                new[] { propertyType });

            var builderForPropertyGetMethod = GetBuilderForPropertyGetMethod(typeBuilder, propertyName, propertyType,
                privateFieldBuilder);
            var builderForPropertySetMethod = GetBuilderForPropertySetMethod(typeBuilder, propertyName, propertyType,
                privateFieldBuilder);

            propertyBuilder.SetGetMethod(builderForPropertyGetMethod);
            propertyBuilder.SetSetMethod(builderForPropertySetMethod);
        }
        
        private static MethodBuilder GetBuilderForPropertyGetMethod(TypeBuilder typeBuilder, string propertyName, Type propertyType, FieldInfo privateFieldBuilder)
        {
            var propertyGetMethodName = "get_" + propertyName;
            var propertyGetMethodBuilder = typeBuilder.DefineMethod(propertyGetMethodName, GetSetAttr, propertyType, new Type[0]);
            ReturnPrivateFieldInMethodBody(privateFieldBuilder, propertyGetMethodBuilder);
            return propertyGetMethodBuilder;
        }

        private static void ReturnPrivateFieldInMethodBody(FieldInfo privateFieldBuilder, MethodBuilder propertyGetMethodBuilder)
        {
            var getMethodIntermediateLanguageGenerator = propertyGetMethodBuilder.GetILGenerator();
            getMethodIntermediateLanguageGenerator.Emit(OpCodes.Ldarg_0);
            getMethodIntermediateLanguageGenerator.Emit(OpCodes.Ldfld, privateFieldBuilder);
            getMethodIntermediateLanguageGenerator.Emit(OpCodes.Ret);
        }

        private static MethodBuilder GetBuilderForPropertySetMethod(TypeBuilder typeBuilder, string propertyName, Type propertyType, FieldInfo privateFieldBuilder)
        {
            var propertySetMethodName = "set_" + propertyName;
            var propertySetMethodBuilder = typeBuilder.DefineMethod(propertySetMethodName, GetSetAttr, null, new[] { propertyType });
            AssignPrivateFieldInMethodBody(privateFieldBuilder, propertySetMethodBuilder);
            return propertySetMethodBuilder;
        }

        private static void AssignPrivateFieldInMethodBody(FieldInfo privateFieldBuilder, MethodBuilder propertySetMethodBuilder)
        {
            var setMethodIntermediateLanguageGenerator = propertySetMethodBuilder.GetILGenerator();
            setMethodIntermediateLanguageGenerator.Emit(OpCodes.Ldarg_0);
            setMethodIntermediateLanguageGenerator.Emit(OpCodes.Ldarg_1);
            setMethodIntermediateLanguageGenerator.Emit(OpCodes.Stfld, privateFieldBuilder);
            setMethodIntermediateLanguageGenerator.Emit(OpCodes.Ret);
        }
    }
}