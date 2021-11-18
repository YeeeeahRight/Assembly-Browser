using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssemblyBrowserCore.Model;

namespace AssemblyBrowserCore
{
    public class AssemblyBrowser
    {
        private const string NoNamespaceString = "No namespace";

        public List<NamespaceMetadata> GetAssemblyData(string path)
        {
            var namespaces = new Dictionary<string, NamespaceMetadata>();
            var assembly = Assembly.LoadFrom(path);
            var assemblyTypes = assembly.GetTypes();

            foreach (var type in assemblyTypes)
            {
                if (namespaces.TryGetValue(type?.Namespace ?? NoNamespaceString, out var namespaceInfo))
                {
                    namespaceInfo.Classes.Add(ClassMetadata.AsClassInfo(type));
                }
                else
                {
                    var namespaceString = type?.Namespace ?? NoNamespaceString;
                    var newNamespace = new NamespaceMetadata(namespaceString);
                    newNamespace.Classes.Add(ClassMetadata.AsClassInfo(type));
                    namespaces.Add(namespaceString, newNamespace);
                }
            }

            return namespaces.Values.ToList();
        }
    }
}