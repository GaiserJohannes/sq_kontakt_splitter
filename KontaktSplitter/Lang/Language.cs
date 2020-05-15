using KontaktSplitter.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace KontaktSplitter.Lang
{
    /// <summary>
    /// Represents an abstract class containing
    /// language specific information and vocabalury
    /// used by the contact splitter in order to
    /// extract a contacts parameters
    /// </summary>
    public abstract class Language
    {
        #region Fields
        /// <summary>
        /// provides language specific configurations
        /// and settings
        /// </summary>
        protected IConfiguration config;
        #endregion


        #region Properties

        /// <summary>
        /// Contains language specidic vocabulary of contact titles.
        /// The list is ordered by the titles priority in descending manner
        /// </summary>
        public IList<Title> Titles { get; set; } = new List<Title>();

        /// <summary>
        /// Contains language specific vocabulary of contact functions
        /// </summary>
        public IList<Function> Functions { get; set; } = new List<Function>();

        /// <summary>
        /// Mapping of genders and their corresponding salutations
        /// </summary>
        public IDictionary<string, Gender> Salutations { get; set; } = new Dictionary<string, Gender>();

        #endregion


        public Language()
        {
            this.config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Langsettings.json", false, true)
                .Build();
        }

        
        /// <summary>
        /// Creates a letter salutation string for the provided contact
        /// object
        /// </summary>
        /// <param name="contact">Contact to build a sulation string for</param>
        /// <param name="function">Optional function of the contact</param>
        /// <returns>Salutation string</returns>
        public abstract string CreateLetterSalutation(Contact contact, string function = null);
    }
}
