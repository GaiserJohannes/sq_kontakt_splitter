using KontaktSplitter.Lang;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace KontaktSplitter.Services
{
    /// <summary>
    /// saves changes to the json settings file
    /// </summary>
    public class JSONConfiguration : IConfiguration
    {
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
