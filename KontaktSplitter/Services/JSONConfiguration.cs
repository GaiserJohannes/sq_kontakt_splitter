using KontaktSplitter.Lang;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace KontaktSplitter.Services
{
    /// <summary>
    /// saves changes to the json settings file
    /// </summary>
    public class JSONConfiguration : ILanguageConfiguration
    {

        private readonly List<Language> languages = new List<Language>();

        /// <summary>
        /// create languages
        /// </summary>
        public JSONConfiguration()
        {
            var german = new German();
            languages.Add(german);

            var english = new English();
            languages.Add(english);

        }

        /// <summary>
        /// get all languages of the config file
        /// </summary>
        /// <returns></returns>
        public List<Language> GetLanguages()
        {
            return languages;
        }

        /// <summary>
        /// saves all titles if the language
        /// </summary>
        /// <param name="language"></param>
        public void UpdateLanguage(Language language)
        {
            try
            {
                var obj = JObject.Parse(File.ReadAllText("Langsettings.json"));
                obj["languages"][language.Name]["titles"] = JArray.FromObject(language.Titles);
                File.WriteAllText("Langsettings.json", obj.ToString(Formatting.Indented));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
