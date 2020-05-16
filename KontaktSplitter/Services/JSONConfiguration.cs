using KontaktSplitter.Lang;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace KontaktSplitter.Services
{
    public class JSONConfiguration : IConfiguration
    {
        public void UpdateSettings(IList<Language> languages)
        {
            var dict = languages.ToDictionary(l => l.Name);
            var temp = new { languages = dict };
            try
            {
                File.WriteAllText("Langsettings.json", JsonSerializer.Serialize(temp, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
