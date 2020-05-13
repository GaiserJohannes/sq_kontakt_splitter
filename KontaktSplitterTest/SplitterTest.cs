using System;
using KontaktSplitter.Lang;
using KontaktSplitter.Model;
using KontaktSplitter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KontaktSplitterTest
{
    [TestClass]
    public class SplitterTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var splitter = new DefaultContactSplitter();

            var actual = splitter.SplitContact("Frau Sandra Berger");

            var expected = new Contact();
            expected.Language = new German();
            expected.Name = "Sandra";
            expected.LastName = "Berger";
            expected.Salutation = "Frau";
            expected.Gender = Gender.FEMALE;
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var splitter = new DefaultContactSplitter();

            var actual = splitter.SplitContact("Herr Dr. Sandro Gutmensch");

            var expected = new Contact();
            expected.Language = new German();
            expected.Title = "Dr.";
            expected.Name = "Sandro";
            expected.LastName = "Gutmensch";
            expected.Salutation = "Herr";
            expected.Gender = Gender.MALE;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var splitter = new DefaultContactSplitter();

            var actual = splitter.SplitContact("Professor Heinreich Freiherr vom Wald");

            var expected = new Contact();
            expected.Language = new German();
            expected.Title = "Professor Freiherr";
            expected.Name = "Heinrich";
            expected.LastName = "vom Wald";
            expected.Gender = Gender.MALE;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var splitter = new DefaultContactSplitter();

            var actual = splitter.SplitContact("Mrs. Doreen Faber");

            var expected = new Contact();
            expected.Language = new English();
            expected.Salutation = "Mrs.";
            expected.Name = "Doreen";
            expected.LastName = "Faber";
            expected.Gender = Gender.FEMALE;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var splitter = new DefaultContactSplitter();

            var actual = splitter.SplitContact("Mme. Charlotte Noir");

            var expected = new Contact();
            expected.Salutation = "Mme.";
            expected.Name = "Charlotte";
            expected.LastName = "Noir";
            expected.Gender = Gender.FEMALE;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var splitter = new DefaultContactSplitter();

            var actual = splitter.SplitContact("Estobar y Gonzales");

            var expected = new Contact();
            //todo wie aufgeteilt?

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var splitter = new DefaultContactSplitter();

            var actual = splitter.SplitContact("Frau Prof. Dr. rer. nat. Maria von Leuthäuser-Schnarrenberger");

            var expected = new Contact();
            expected.Language = new German();
            expected.Salutation = "Frau";
            expected.Title = "Prof. Dr. rer. nat.";
            expected.Name = "Maria";
            expected.LastName = "von Leuthäuser-Schnarrenberger";
            expected.Gender = Gender.FEMALE;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod8()
        {
            var splitter = new DefaultContactSplitter();

            var actual = splitter.SplitContact("Herr Dipl. Ing. Max von Müller");

            var expected = new Contact();
            expected.Language = new German();
            expected.Salutation = "Herr";
            expected.Title = "Dipl. Ing.";
            expected.Name = "Max";
            expected.LastName = "von Müller";
            expected.Gender = Gender.MALE;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod9()
        {
            var splitter = new DefaultContactSplitter();

            var actual = splitter.SplitContact("Dr. Russwurm, Winfried");

            var expected = new Contact();
            expected.Language = new German();
            expected.Title = "Dr.";
            expected.Name = "Winfried";
            expected.LastName = "Russwurm";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod10()
        {
            var splitter = new DefaultContactSplitter();

            var actual = splitter.SplitContact("Herr Dr.-Ing. Dr. rer. nat. Dr. h.c. mult. Paul Steffens");

            var expected = new Contact();
            expected.Language = new German();
            expected.Salutation = "Herr";
            expected.Title = "Dr.-Ing. Dr. rer. nat. Dr. h.c. mult.";
            expected.Name = "Paul";
            expected.LastName = "Steffens";
            expected.Gender = Gender.MALE;

            Assert.AreEqual(expected, actual);
        }
    }
}
