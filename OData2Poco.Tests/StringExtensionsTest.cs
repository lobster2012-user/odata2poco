﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OData2Poco.Extensions;

namespace OData2Poco.Tests
{
    [TestFixture]
    class StringExtensionsTest
    {
        [Test]
        [TestCase("userName", "UserName")]
        [TestCase("user_Name", "UserName")]
        [TestCase("user Name", "UserName")]
        [TestCase("user-Name", "UserName")]
        public void ToPascalCaseTest(string name, string pascalName)
        {
            Assert.AreEqual(pascalName, name.ToPascalCase());
        }

        [Test]
        [TestCase("UserName", "userName")]
        [TestCase("user_Name", "userName")]
        [TestCase("User Name", "userName")]
        [TestCase("user-Name", "userName")]
        public void ToCamelCaseTest(string name, string pascalName)
        {
            Assert.AreEqual(pascalName, name.ToCamelCase());
        }




        [Test]
        public void TrimAllSpaceAndCrLfTest()
        {
            var text = @"this            is  line1


          and this is     line2";
            //Console.WriteLine(text);
            var expected = "this is line1 and this is line2";
            // Console.WriteLine(text.TrimAllSpace());
            Assert.AreEqual(text.TrimAllSpace(), expected);
        }

        [Test]
        public void TrimAllSpaceAndKeepCrLfTest()
        {
            //var text = "this            is  line1   \n\nand this is     line2";
            var text = @"this            is  line1


          and this is     line2";
            var expected = "this is line1\nand this is line2\n";


            //  Console.WriteLine("expected:\n{0}",expected);
            //var expected = "this is line1 and this is line2";
            //Console.WriteLine(text.TrimAllSpace(true));
            Assert.AreEqual(text.TrimAllSpace(true), expected);
        }
        [Test]
        [TestCase("Apple", "apple")]
        [TestCase("apple", "Apple")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void ToggleFirstLetterTest(string name, string expected)
        {
            var name2 = name.ToggleFirstLetter();
            Assert.That(name2, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("event", "Event")]
        [TestCase("void", "Void")]
        [TestCase("Void", "Void")]
        [TestCase("char", "Char")]
        [TestCase("Char", "Char")]
        [TestCase("not_reserved", "not_reserved")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void ChangeReservedWordTest(string name, string expected)
        {
            var name2 = name.ChangeReservedWord();
            Assert.That(name2, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("camel", CaseEnum.Camel)]
        [TestCase("CAMEL", CaseEnum.Camel)]
        [TestCase("pas", CaseEnum.Pas)]
        [TestCase("anyvalue", CaseEnum.None)]
        public void StringToCaseEnumTest(string val, CaseEnum expected)
        {
            var enumValue = val.ToEnum<CaseEnum>();
            //Assert
            Assert.That(enumValue, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("basic", AuthenticationType.Basic)]
        [TestCase("BASIC", AuthenticationType.Basic)]
        [TestCase("undefined", AuthenticationType.None)]
        public void AuthenticationTypeTest(string auth, AuthenticationType expected)
        {
            //Arrange
            //Act
            AuthenticationType? authType = auth.ToEnum<AuthenticationType>();
            //Assert
            Assert.That(authType, Is.EqualTo(expected));
        }
    }
}
