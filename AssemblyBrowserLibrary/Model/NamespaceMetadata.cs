using System.Collections.Generic;

namespace AssemblyBrowserCore.Model
{
    public class NamespaceMetadata
    {
        public string Name { get; set; }
        public List<ClassMetadata> Classes { get; set; }

        public NamespaceMetadata(string name)
        {
            Name = name;
            Classes = new List<ClassMetadata>();
        }
    }
}