using KontaktSplitter.Lang;
using KontaktSplitter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KontaktSplitterTest
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void SaveSettings()
        {
            var configuration = new JSONConfiguration();
            var german = new German();
            german.Titles.Add("Test");

            configuration.UpdateLanguage(german);

            Assert.IsTrue(german.Titles.Contains("Test"));
        }
    }
}
