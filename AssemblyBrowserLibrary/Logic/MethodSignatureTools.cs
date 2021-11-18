using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserCore.Logic
{
    public static class MethodSignatureTools
    {
        public static string GetSignature(this MethodInfo method)
        {
            var signatureBuilder = new StringBuilder();

            signatureBuilder.Append(GetMethodAccessorSignature(method));
            signatureBuilder.Append(" ");

            signatureBuilder.Append(method.Name);

            if (method.IsGenericMethod)
            {
                signatureBuilder.Append(GetGenericSignature(method));
            }

            signatureBuilder.Append(GetMethodArgumentsSignature(method));

            return signatureBuilder.ToString();
        }

        public static string GetMethodAccessorSignature(this MethodInfo method)
        {
            string signature = null;

            if (method.IsAssembly)
            {
                signature = "<internal> ";

                if (method.IsFamily)
                    signature += "<protected> ";
            }
            else if (method.IsPublic)
            {
                signature = "<public> ";
            }
            else if (method.IsPrivate)
            {
                signature = "<private> ";
            }
            else if (method.IsFamily)
            {
                signature = "<protected> ";
            }

            if (method.IsStatic)
                signature += "static ";

            signature += TypeSignatureTools.GetSignature(method.ReturnType);

            return signature;
        }

        public static string GetGenericSignature(this MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (!method.IsGenericMethod) throw new ArgumentException($"{method.Name} is not generic.");

            return TypeSignatureTools.BuildGenericSignature(method.GetGenericArguments());
        }

        public static string GetMethodArgumentsSignature(this MethodInfo method)
        {
            var isExtensionMethod = method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false);
            var methodParameters = method.GetParameters().AsEnumerable();

            if (isExtensionMethod)
            {
                methodParameters = methodParameters.Skip(1);
            }

            var methodParameterSignatures = methodParameters.Select(param =>
            {
                var signature = string.Empty;

                if (param.ParameterType.IsByRef)
                    signature = "ref ";
                else if (isExtensionMethod && param.Position == 0)
                    signature = "this ";

                signature += TypeSignatureTools.GetSignature(param.ParameterType) + " ";

                signature += param.Name;

                return signature;
            });

            var methodParameterString = "(" + string.Join(", ", methodParameterSignatures) + ")";

            return methodParameterString;
        }
    }
}