using System.Reflection;
using System.Text;

namespace AssemblyBrowserCore.Logic
{
    public static class FieldSignatureTools
    {
        public static string GetSignature(this FieldInfo fieldInfo)
        {
            var signatureBuilder = new StringBuilder(fieldInfo.GetAccessModifierSignature());
            signatureBuilder.Append(" ");
            signatureBuilder.Append(fieldInfo.FieldType.GetSignature());
            signatureBuilder.Append(" ");
            signatureBuilder.Append(fieldInfo.Name);

            return signatureBuilder.ToString();
        }

        private static string GetAccessModifierSignature(this FieldInfo fieldInfo)
        {
            if (fieldInfo.IsPrivate) return "<private>";
            if (fieldInfo.IsPublic) return "<public>";
            if (fieldInfo.IsAssembly) return "<internal>";
            if (fieldInfo.IsFamilyAndAssembly) return "<private protected>";
            return "<protected internal>";
        }
    }
}