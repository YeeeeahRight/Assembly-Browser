using System;
using System.Collections.Generic;
using System.Reflection;
using AssemblyBrowserCore.Logic;
using NUnit.Framework;

namespace AssemblyBrowserTest
{
    [TestFixture]
    public class Tests
    {
        private const BindingFlags BindingFlagsAll = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;
        
        [Test]
        public void TestMethodSignatureTool()
        {
            var method = typeof(TestClass).GetMethods(BindingFlagsAll)[2];
            Assert.AreEqual("<public> void TestMethod(List<List<int>> list, int a, int b)",
                method.GetSignature());
        }
        
        [Test]
        public void TestFieldSignatureTool()
        {
            var field = typeof(TestClass).GetFields(BindingFlagsAll)[0];
            Assert.AreEqual("<private> Double _testField",
                field.GetSignature());
        }
        
        [Test]
        public void TestConstructorSignatureTool()
        {
            var constructor = typeof(TestClass).GetConstructors(BindingFlagsAll)[0];
            Assert.AreEqual("<public>  .ctor(Double testField)",
                constructor.GetSignature());
        }
        
        [Test]
        public void TestPropertySignatureTool()
        {
            var property = typeof(TestClass).GetProperties(BindingFlagsAll)[0];
            Assert.AreEqual("<public>  string TestProperty  { get_TestProperty } ",
                property.GetSignature());
        }
    }

    public class TestClass
    {
        private double _testField;
        
        public string TestProperty { get; private set; }

        public TestClass(double testField)
        {
            _testField = testField;
        }

        public void TestMethod(List<List<int>> list, int a, int b)
        {
            a = b;
            b = a;
        }
    }
}