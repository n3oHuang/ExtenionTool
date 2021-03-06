using System;
using System.Collections.Generic;
using ExtensionTool;
using NUnit.Framework;

namespace ExtensionTool.Tests
{
    public enum e_Test
    {
        A = 1,
        B = 2,
        C = 4,
        D = 8,
        E = 16,
        F = 32,
        G = 64
    }

    [TestFixture]
    public class ExteionEnumTest
    {
        [Test]
        public void ExteionEnum_TestMatch_True()
        {
            string[] recept = new string[] { "A", "B", "C", "F" };

            e_Test c1 = (e_Test.A | e_Test.B | e_Test.C | e_Test.F);

            var result = c1.GetEnumString();

            Assert.AreEqual(recept, result);
        }

        [Test]
        public void ExteionEnum_TestEmpty_True()
        {
            string[] recept = new string[] { };

            e_Test c1 = new e_Test();

            var result = c1.GetEnumString();

            Assert.AreEqual(recept, result);
        }

        [Test]
        public void ExteionEnum_TestNotMatch_True()
        {
            string[] recept = new string[] { "A", "B", "C", "F" };

            e_Test c1 = (e_Test.A | e_Test.B | e_Test.F);

            var result = c1.GetEnumString();

            Assert.AreNotEqual(recept, result);
        }

        [Test]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentException))]
        public void ExteionEnum_TestNotEnum_True()
        {
            int a = 1;
            double d = 1.0;
            byte b = 1;
            char c = 'a';

            TestNotEnum(a);
            TestNotEnum(d);
            TestNotEnum(b);
            TestNotEnum(c);
        }

        private void TestNotEnum<T>(T value) where T : struct, IConvertible
        {
            value.GetEnumString();
        }

        [Test()]
        public void CheckAuth_TestInt_True()
        {
            e_Type ownerAuth = (e_Type)1;
            e_Type auth = e_Type.A;

            bool except = true;

            bool result = auth.CheckAuth(ownerAuth, (a, b) => { return (a & b) > 0; });

            Assert.AreEqual(except, result);
        }
    }
}