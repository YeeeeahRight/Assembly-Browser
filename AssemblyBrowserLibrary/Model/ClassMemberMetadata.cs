using System;
using System.Reflection;
using AssemblyBrowserCore.Logic;

namespace AssemblyBrowserCore.Model
{
    public class ClassMemberMetadata
    {
        public string StringRepresentation { get; set; }

        public ClassMemberMetadata(string stringRepresentation)
        {
            StringRepresentation = stringRepresentation;
        }

        public static ClassMemberMetadata FieldAsClassMemberInfo(FieldInfo fieldInfo)
        {
            return new ClassMemberMetadata("[FIELD]: " + fieldInfo.GetSignature());
        }

        public static ClassMemberMetadata ConstructorAsClassMemberInfo(ConstructorInfo constructorInfo)
        {
            return new ClassMemberMetadata("[CONSTRUCTOR]: " + constructorInfo.GetSignature());
        }
        
        public static ClassMemberMetadata MethodAsClassMemberInfo(MethodInfo methodInfo)
        {
            return new ClassMemberMetadata("[METHOD]: " + methodInfo.GetSignature());
        }
        
        public static ClassMemberMetadata PropertyAsClassMemberInfo(PropertyInfo propertyInfo)
        {
            return new ClassMemberMetadata("[PROPERTY]: " + propertyInfo.GetSignature());
        }
    }
}