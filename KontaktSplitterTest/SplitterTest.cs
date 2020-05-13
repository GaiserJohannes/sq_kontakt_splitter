using System;
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
            expected.Title = "Professor Freiherr";
            expected.Name = "Heinrich";
            expected.LastName = "vom Wald";
            expected.Gender = Gender.MALE;

            Assert.AreEqual(expected, actual);
        }
    }
}
