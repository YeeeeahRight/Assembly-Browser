using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyBrowserCore.Logic
{
    public static class TypeSignatureTools
    {
       
        public static string GetSignature(this Type type) {
            var isGenericType = TypeExtensionMethods.IsGeneric(type);

            var signature = TypeExtensionMethods.GetQualifiedTypeName(type);

            if (isGenericType) {
                signature += BuildGenericSignature(type.GetGenericArguments());
            }
            
            return signature;
        }

        public static string BuildGenericSignature(IEnumerable<Type> genericArgumentTypes) {
            var argumentSignatures = genericArgumentTypes.Select(GetSignature);

            return "<" + string.Join(", ", argumentSignatures) + ">";
        }
    }
}