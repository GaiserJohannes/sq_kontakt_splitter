using KontaktSplitter.Lang;
using KontaktSplitter.Model;
using KontaktSplitter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;

namespace KontaktSplitterTest
{
    [TestClass]
    public class SplitterTest { 

        private DefaultContactSplitter splitter;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        
        [TestInitialize]
        public void Init()
        {
            splitter = new DefaultContactSplitter();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var expected = new Contact();
            expected.Language = new German();
            expected.Name = "Sandra";
            expected.LastName = "Berger";
            expected.Salutation = "Frau";
            expected.Gender = Gender.FEMALE;

            //Act
            var actual = splitter.SplitContact("Frau Sandra Berger");

            //Assert
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            var expected = new Contact();
            expected.Language = new German();
            expected.Title.Add("Dr.");
            expected.Name = "Sandro";
            expected.LastName = "Gutmensch";
            expected.Salutation = "Herr";
            expected.Gender = Gender.MALE;

            //Act
            var actual = splitter.SplitContact("Herr Dr. Sandro Gutmensch");

            //Assert
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            var expected = new Contact();
            expected.Language = new German();
            expected.Title.Add("Professor");
            expected.Name = "Heinreich";
            expected.LastName = "Freiherr vom Wald";

            //Act
            var actual = splitter.SplitContact("Professor Heinreich Freiherr vom Wald");

            //Assert
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            //Assert
            var expected = new Contact();
            expected.Language = new English();
            expected.Salutation = "Mrs.";
            expected.Name = "Doreen";
            expected.LastName = "Faber";
            expected.Gender = Gender.FEMALE;

            //Act
            var actual = splitter.SplitContact("Mrs. Doreen Faber");
            
            //Arrange
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod5()
        {
            //Arrange
            //Französische Sprache ist noch nicht bekannt, deshalb
            var expected = new Contact();
            expected.Language = new German();   //Deutsch ist Standard, falls er die Sprache anhand von Titel oder Anrede nicht bestimmen kann
            expected.Name = "Mme.";
            expected.LastName = "Charlotte Noir";
            //statt
            //var expected = new Contact();
            //expected.Language = new French();
            //expected.Salutation = "Mme.";
            //expected.Name = "Charlotte";
            //expected.LastName = "Noir";
            //expected.Gender = Gender.FEMALE;

            //Act
            var actual = splitter.SplitContact("Mme. Charlotte Noir");

            //Assert
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod6()
        {
            //Arrange
            //Spanische Sprache ist noch nicht bekannt, deshalb
            var expected = new Contact();
            expected.Language = new German();
            expected.Name = "Estobar";
            expected.LastName = "y Gonzales";
            //statt
            //var expected = new Contact();
            //expected.Language = new Espanol();
            //expected.Name = "Estobar";
            //expected.LastName = "y Gonzales";

            //Act
            var actual = splitter.SplitContact("Estobar y Gonzales");

            //Assert
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod7()
        {
            //Arrange
            var expected = new Contact();
            expected.Language = new German();
            expected.Salutation = "Frau";
            expected.Title.Add("Prof.");
            expected.Title.Add("Dr. rer. nat.");
            expected.Name = "Maria";
            expected.LastName = "von Leuthäuser-Schnarrenberger";
            expected.Gender = Gender.FEMALE;

            //Act
            var actual = splitter.SplitContact("Frau Prof. Dr. rer. nat. Maria von Leuthäuser-Schnarrenberger");

            //Assert
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod8()
        {
            //Arrange
            var expected = new Contact();
            expected.Language = new German();
            expected.Salutation = "Herr";
            expected.Title.Add("Dipl. Ing.");
            expected.Name = "Max";
            expected.LastName = "von Müller";
            expected.Gender = Gender.MALE;

            //Act
            var actual = splitter.SplitContact("Herr Dipl. Ing. Max von Müller");

            //Assert
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod9()
        {
            //Arrange
            var expected = new Contact();
            expected.Language = new German();
            expected.Title.Add("Dr.");
            expected.Name = "Winfried";
            expected.LastName = "Russwurm";

            //Act
            var actual = splitter.SplitContact("Dr. Russwurm, Winfried");

            //Assert
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod10()
        {
            //Arrange
            var expected = new Contact();
            expected.Language = new German();
            expected.Salutation = "Herr";
            expected.Title.Add("Dr. h.c. mult.");
            expected.Title.Add("Dr. rer. nat.");
            expected.Title.Add("Dr.-Ing.");
            expected.Name = "Paul";
            expected.LastName = "Steffens";
            expected.Gender = Gender.MALE;

            //Act
            var actual = splitter.SplitContact("Herr Dr.-Ing. Dr. rer. nat. Dr. h.c. mult. Paul Steffens");

            //Assert
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TitleOrderTest()
        {
            //Arrange
            var expected = new Contact();
            expected.Language = new German();
            expected.Salutation = "Herr";
            expected.Title.Add("Professor");
            expected.Title.Add("Dr. h.c. mult.");
            expected.Title.Add("Dr. rer. nat.");
            expected.Title.Add("Dr.-Ing.");
            expected.Name = "Paul";
            expected.LastName = "Steffens";
            expected.Gender = Gender.MALE;

            //Act
            var actual = splitter.SplitContact("Herr Dr.-Ing. Dr. rer. nat. Professor Dr. h.c. mult. Paul Steffens");

            //Assert
            TestContext.WriteLine("expected: " + expected.ToString());
            TestContext.WriteLine("actual: " + expected.ToString());
            TestContext.WriteLine("equal? " + actual.Equals(expected));
            Assert.AreEqual(expected, actual);
        }
    }
}
