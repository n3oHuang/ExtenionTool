﻿using NUnit.Framework;
using ExtensionTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ExtensionTool.Tests
{
    public class DisplayAttribute : Attribute
    {
        public string Name { get; set; }
    }

    public enum e_Type
    {
        [Display(Name = nameof(A))]
        A = 1,

        [Display(Name = nameof(B))]
        B = 2,

        [Display(Name = nameof(C))]
        C = 4
    }

    public class Tag1 : Attribute
    {
        public Tag1(string v)
        {
            Value = v;
        }

        public string Value { get; set; }
    }

    [Tag1("1234")]
    public class WithTag { }

    public class NoTag { }

    [TestFixture()]
    public class ExtensionAttributeTests
    {
        [Test()]
        public void Test_WithTagCurrectValue_True()
        {
            string recept = "1234";

            string result = typeof(WithTag).GetAttributeValue((Tag1 tag) => tag.Value);

            Assert.AreEqual(result, recept);
        }

        [Test()]
        public void Test_NOTage_True()
        {
            string recpt = default(string);

            string result = typeof(NoTag).GetAttributeValue((Tag1 tag) => tag.Value);

            Assert.AreEqual(result, recpt);
        }

        [Test]
        [TestCaseSource("GetEnumTest")]
        public void CheckAttribute_True(TestEnum tester)
        {
            tester.Test();
        }

        public static IEnumerable<TestEnum> GetEnumTest()
        {
            yield return new TestEnum()
            {
                Except = nameof(e_Type.A),
                type = e_Type.A
            };
            yield return new TestEnum()
            {
                Except = nameof(e_Type.B),
                type = e_Type.B
            };
            yield return new TestEnum()
            {
                Except = nameof(e_Type.C),
                type = e_Type.C
            };
        }
    }

    public class TestEnum
    {
        public e_Type type { get; set; }
        public string Except { get; set; }

        public void Test()
        {
            string result = typeof(e_Type).GetField(type.ToString())
                .GetAttributeValue((DisplayAttribute attr) => attr.Name);

            Assert.AreEqual(Except, result);
        }
    }
}