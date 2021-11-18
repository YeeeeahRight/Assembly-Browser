using System;

namespace AssemblyBrowserCore.Logic
{
    public static class TypeExtensionMethods
    {
        public static bool IsGeneric(this Type type) {
            return type.IsGenericType;
        }
        
        public static string GetQualifiedTypeName(Type type) {
            switch (type.Name) {
                case "String":
                    return "string";
                case "Int32":
                    return "int";
                case "Decimal":
                    return "decimal";
                case "Object":
                    return "object";
                case "Void":
                    return "void";
                case "Boolean":
                    return "bool";
            }

            String signature = type.Name;
            
            if(IsGeneric(type))
                signature = RemoveGenericTypeNameArgumentCount(signature);

            return signature;
        }

        
        private static string RemoveGenericTypeNameArgumentCount(string genericTypeSignature) {
            return  genericTypeSignature.Substring(0, genericTypeSignature.IndexOf('`'));
        }
    }
}