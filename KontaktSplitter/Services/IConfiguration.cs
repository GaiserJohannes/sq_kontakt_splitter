using KontaktSplitter.Lang;
using System.Collections.Generic;

namespace KontaktSplitter.Services
{
    interface IConfiguration
    {
        void UpdateSettings(IList<Language> languages);
    }
}
