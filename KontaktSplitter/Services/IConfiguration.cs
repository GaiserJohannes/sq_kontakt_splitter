using KontaktSplitter.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktSplitter.Services
{
    interface IConfiguration
    {
        List<Language> GetLanguages();
        void UpdateLanguage(Language language);
    }
}
