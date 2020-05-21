using KontaktSplitter.Lang;
using KontaktSplitter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KontaktSplitterTest
{
    [TestClass]
    public class ConfigurationTest { 

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
    
        [TestMethod]
        public void SaveSettings()
        {
            //Arrange
            var configuration = new JSONConfiguration();
            var german = new German();
            german.Titles.Add("Test");

            //Act
            configuration.UpdateLanguage(german);

            //Assert
            TestContext.WriteLine("contains Test? " + configuration.GetLanguages()[0].Titles.Contains("Test"));
            Assert.IsTrue(configuration.GetLanguages()[0].Titles.Contains("Test"));
        }
    }
}
