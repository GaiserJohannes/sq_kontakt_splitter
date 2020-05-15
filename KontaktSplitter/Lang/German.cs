using KontaktSplitter.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KontaktSplitter.Lang
{
    /// <summary>
    /// Contains information
    /// and vocabulary of the german
    /// language
    /// </summary>
    public class German : Language
    {
        /// <summary>
        /// Langauge specific settings
        /// </summary>
        private IConfigurationSection langConfig;


        public German()
        {
            /*Load language settings from the configuraton file*/
            langConfig = config.GetSection("languages:german:salut");
            Titles = config.GetSection("languages:german:titles").Get<List<Title>>();
            Functions = config.GetSection("languages:german:functions").Get<List<Function>>();
            Salutations = config.GetSection("languages:german:salutaitons").Get<Dictionary<string, Gender>>();
        }


        public override string CreateLetterSalutation(Contact contact, string function = null)
        {
            var highestTitle = GetHighestAcademicTitle(contact);
            var salutation = GetSalutation(contact.Gender);

            var sb = new StringBuilder()
                .Append(salutation).Append(" ")
                .AppendIf(function, UseIfProvided, function)
                .AppendIf(" ", UseIfProvided, function)
                .AppendIf(highestTitle, UseIfProvided, highestTitle)
                .AppendIf(" ", UseIfProvided, highestTitle)
                .Append(contact.LastName).Append(" ");

            return sb.ToString().Trim();
        }


        /// <summary>
        /// Retrieves the corresponding 
        /// salutation of a given gender
        /// from the configuration file
        /// </summary>
        /// <param name="gender">Gender to 
        /// retrieve a salutation for</param>
        /// <returns>Salutation string</returns>
        private string GetSalutation(Gender gender)
        {
            switch (gender)
            {
                case Gender.MALE:
                    return langConfig[gender.ToString()];
                case Gender.FEMALE:
                    return langConfig[gender.ToString()];
                default:
                    return langConfig["UNKNOWN"];
            }
        }


        /// <summary>
        /// Predicate that verifies if
        /// the given string provides
        /// a valid information or whether
        /// it is unset and should not be used
        /// as part of the salutation
        /// </summary>
        /// <param name="parameter">String to verify</param>
        /// <returns>True if the string provides an
        /// actual content, false otherwise</returns>
        private bool UseIfProvided(string parameter)
        {
            return String.IsNullOrEmpty(parameter) ? false : true;
        }


        /// <summary>
        /// Retrieves the highest academic title
        /// if the specified contact
        /// </summary>
        /// <param name="contact">Contact to retrieve
        /// its highest academic title for</param>
        /// <returns>String representing the contacts
        /// highest academic title</returns>
        private string GetHighestAcademicTitle(Contact contact)
        {
            if (String.IsNullOrEmpty(contact.Title)) return null;

            string[] titles = contact.Title.Split(new char[0]);
            var query = from t in titles
                        group t by Titles.IndexOf(t.Trim()) into g                      //Get priority of each title provided by the contact
                        orderby g.Key                                                   //Order titles by their priority
                        where g.Key >= 0                                                //-1 represents an unknwon title to the system, thus it is ignored
                        select g;

            string highestTitle = query.SelectMany(group => group).FirstOrDefault();    //Only use the highest title which corresponds to the first list item
            return highestTitle;
        }
    }
}
