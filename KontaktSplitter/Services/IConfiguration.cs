using KontaktSplitter.Lang;

namespace KontaktSplitter.Services
{
    interface IConfiguration
    {
        /// <summary>
        /// saves the passed language persistent
        /// </summary>
        /// <param name="language"></param>
        void UpdateLanguage(Language language);
    }
}
