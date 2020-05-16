using KontaktSplitter.Lang;
using KontaktSplitter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            german.Salutations.Add("Test", KontaktSplitter.Model.Gender.FEMALE);

            configuration.UpdateSettings(new List<Language> { german });
        }
    }
}
