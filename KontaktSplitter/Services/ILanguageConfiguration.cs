using KontaktSplitter.Lang;
using System.Collections.Generic;

namespace KontaktSplitter.Services
{
    interface ILanguageConfiguration
    {
        /// <summary>
        /// read languages of configuration
        /// </summary>
        /// <returns></returns>
        List<Language> GetLanguages();

        /// <summary>
        /// saves the passed language persistent
        /// </summary>
        /// <param name="language"></param>
        void UpdateLanguage(Language language);
    }
}
