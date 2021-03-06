﻿using System.Collections.Generic;
using KontaktSplitter.Lang;
using KontaktSplitter.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;

namespace KontaktSplitterTest
{
    [TestClass]
    public class GermanLanguageTest
    {
        private Language lang;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init()
        {
            /*Setup a language model and fill Academic Titles in 
             order of their priority*/
            lang = new German();
        }


        [TestMethod]
        public void TestCommonerName()
        {
            /*Arrange: Create a new female commoner name and german language*/
            var contact = new Contact()
            {
                Language = lang,
                Name = "Sandra",
                LastName = "Berger",
                Salutation = "Frau",
                Gender = Gender.FEMALE
            };

            /*Run: Use the language utilities to create a letter salutation of the
             corresponding contact*/
            string salut = lang.CreateLetterSalutation(contact);


            /*Assert: Providing a commoner name, no titles and function shall be used*/
            string expected = "Sehr geehrte Frau Berger";
            TestContext.WriteLine("expected: " + expected);
            TestContext.WriteLine("actual: " + salut);
            TestContext.WriteLine("equal? " + salut.Equals(expected));
            Assert.AreEqual(expected, salut);
        }


        [TestMethod]
        public void TestAcamdicName()
        {
            /*Arrange: Create a new academic name and german language*/
            var contact = new Contact()
            {
                Language = lang,
                Name = "Paul",
                LastName = "Steffens",
                Salutation = "Herr",
                Title = new ObservableCollection<string>()
                {
                    "Dr.",
                    "Ing.",
                    "Dr. rer. nat.",
                    "Dr. h.c. mult."
                },
                Gender = Gender.MALE
            };

            /*Run: Use the language utilities to create a letter salutation of the
             corresponding contact*/
            string salut = lang.CreateLetterSalutation(contact);


            /*Assert: Providing a academic name, only the highest academic
             tidle shall be used within the salutation*/
            string expected = "Sehr geehrter Herr Dr. Steffens";
            TestContext.WriteLine("expected: " + expected);
            TestContext.WriteLine("actual: " + salut);
            TestContext.WriteLine("equal? " + salut.Equals(expected));
            Assert.AreEqual(expected, salut);
        }


        [TestMethod]
        public void TestAcamdicName_Female()
        {
            /*Arrange: Create a new female academic name and german language*/
            var contact = new Contact()
            {
                Language = lang,
                Name = "Maria",
                LastName = "von Leuthäuser-Schnarrenberger",
                Salutation = "Frau",
                Title = new ObservableCollection<string>()
                {
                    "Prof.",
                    "Dr.",
                    "Ing.",
                    "Dr. rer. nat.",
                    "Dr. h.c. mult."
                },
                Gender = Gender.FEMALE
            };

            /*Run: Use the language utilities to create a letter salutation of the
             corresponding contact*/
            string salut = lang.CreateLetterSalutation(contact);


            /*Assert: Providing a academic name, only the highest academic
             tidle shall be used within the salutation*/
            string expected = "Sehr geehrte Frau Prof. von Leuthäuser-Schnarrenberger";
            TestContext.WriteLine("expected: " + expected);
            TestContext.WriteLine("actual: " + salut);
            TestContext.WriteLine("equal? " + salut.Equals(expected));
            Assert.AreEqual(expected, salut);
        }


        [TestMethod]
        public void TestDiverseGender()
        {
            /*Arrange: Create a new contact having diverse gender and german language*/
            var contact = new Contact()
            {
                Language = lang,
                Name = "Fawny",
                LastName = "Weniger-Viel",
                Salutation = string.Empty,
                Gender = Gender.DIVERSE
            };

            /*Run: Use the language utilities to create a letter salutation of the
             corresponding contact*/
            string salut = lang.CreateLetterSalutation(contact);


            /*Assert: Providing a diverse gender, the gender gap
             symbol has to be used for the greeting. Furthermore, 
             no salutation like 'Herr' or 'Frau' are used*/
            string expected = "Sehr geehrte*r Weniger-Viel";
            TestContext.WriteLine("expected: " + expected);
            TestContext.WriteLine("actual: " + salut);
            TestContext.WriteLine("equal? " + salut.Equals(expected));
            Assert.AreEqual(expected, salut);
        }



        [TestMethod]
        public void TestNobleMan()
        {
            /*Arrange: Create a new contact having a noble titiel*/
            var contact = new Contact()
            {
                Language = lang,
                Name = " Heinreich",
                LastName = "Freiherr vom Wald",
                Title = new ObservableCollection<string>()
                {
                    "Professor"
                },
                Salutation = string.Empty,
                Gender = Gender.MALE
            };

            /*Run: Use the language utilities to create a letter salutation of the
             corresponding contact*/
            string salut = lang.CreateLetterSalutation(contact);


            /*Assert: Providing a noble title gender, the title
             should be used as part of the letter salutation*/
            string expected = "Sehr geehrter Herr Professor Freiherr vom Wald";
            TestContext.WriteLine("expected: " + expected);
            TestContext.WriteLine("actual: " + salut);
            TestContext.WriteLine("equal? " + salut.Equals(expected));
            Assert.AreEqual(expected, salut);
        }
    }
}
