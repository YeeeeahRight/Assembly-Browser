using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserCore.Model
{
    public class ClassMetadata
    {
        private const BindingFlags BindingFlagsAll = BindingFlags.Instance | 
                                                     BindingFlags.NonPublic | 
                                                     BindingFlags.Static | 
                                                     BindingFlags.Public | 
                                                     BindingFlags.FlattenHierarchy;

        public string Name { get; set; }
        public List<ClassMemberMetadata> Members { get; set; }

        public ClassMetadata(string name)
        {
            Name = name;
            Members = new List<ClassMemberMetadata>();
        }

        public static ClassMetadata AsClassInfo(Type type)
        {
            var classInfo = new ClassMetadata(GetClassSignature(type));

            var fields = type.GetFields(BindingFlagsAll);
            foreach (var fieldInfo in fields)
            {
                classInfo.Members.Add(ClassMemberMetadata.FieldAsClassMemberInfo(fieldInfo));
            }

            var constructors = type.GetConstructors(BindingFlagsAll);
            foreach (var constructorInfo in constructors)
            {
                classInfo.Members.Add(ClassMemberMetadata.ConstructorAsClassMemberInfo(constructorInfo));
            }
            
            var methods = type.GetMethods(BindingFlagsAll);
            foreach (var methodInfo in methods)
            {
                classInfo.Members.Add(ClassMemberMetadata.MethodAsClassMemberInfo(methodInfo));
            }

            var properties = type.GetProperties(BindingFlagsAll);
            foreach (var propertyInfo in properties)
            {
                classInfo.Members.Add(ClassMemberMetadata.PropertyAsClassMemberInfo(propertyInfo));
            }
            
            return classInfo;
        }

        private static string GetClassSignature(Type type)
        {
            StringBuilder classSignatureBuilder = new StringBuilder("[CLASS] ");
            if (type.IsNotPublic) 
                classSignatureBuilder.Append("<internal> ");
            else 
                classSignatureBuilder.Append("<public> ");
            if (type.IsAbstract) classSignatureBuilder.Append("<abstract> ");
            if (type.IsSealed) classSignatureBuilder.Append("<sealed> ");
            if (type.IsClass && type.BaseType.Name == "MulticastDelegate") classSignatureBuilder.Append("delegate");
            if (type.IsClass) classSignatureBuilder.Append("class");
            if (type.IsInterface) classSignatureBuilder.Append("interface");
            if (type.IsEnum) classSignatureBuilder.Append("enum");
            if (type.IsValueType && !type.IsPrimitive) classSignatureBuilder.Append("structure");
            classSignatureBuilder.Append(" ");
            classSignatureBuilder.Append(type.Name);
            return classSignatureBuilder.ToString();
        }
    }
}