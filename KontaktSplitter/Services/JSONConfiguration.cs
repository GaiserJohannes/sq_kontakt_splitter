using KontaktSplitter.Lang;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KontaktSplitter.Services
{
    public class JSONConfiguration : IConfiguration
    {
        private List<Language> CachedLanguage;

        public List<Language> GetLanguages()
        {
            if (CachedLanguage == null)
            {
                CachedLanguage = JsonSerializer.Deserialize<List<Language>>(File.ReadAllText(""));
            }

            return CachedLanguage;
        }

        public void UpdateLanguage(Language language)
        {
            throw new NotImplementedException();
        }
    }
}
