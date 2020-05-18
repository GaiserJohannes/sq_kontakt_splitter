using KontaktSplitter.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

        /*Object representation of the json file*/
        private JObject jsonConfig;

        public German()
        {
            /*Load language settings from the configuraton file*/
            Name = "german";
            langConfig = config.GetSection($"languages:{Name}:salut");

            LoadConfiguation();
        }


        /// <summary>
        /// Retrieves configuration from the settings.json file
        /// </summary>
        private void LoadConfiguation()
        {
            using (var reader = new StreamReader("Langsettings.json"))
            {
                var json = reader.ReadToEnd();
                this.jsonConfig = JObject.Parse(json);

                JsonConvert.DefaultSettings = (() =>
                {
                    var settings = new JsonSerializerSettings();
                    settings.Converters.Add(new StringEnumConverter());
                    return settings;
                });

                Titles = JsonConvert.DeserializeObject<List<string>>(GetJsonProperty($"languages:{Name}:titles"));
                Functions = JsonConvert.DeserializeObject<List<Function>>(GetJsonProperty($"languages:{Name}:functions"));
                Salutations = JsonConvert.DeserializeObject<Dictionary<string, Gender>>(GetJsonProperty($"languages:{Name}:salutations"));
            }
        }

        /// <summary>
        /// Creates a german letter salutation string for the provided contact
        /// object
        /// </summary>
        /// <param name="contact">Contact to build a sulation string for</param>
        /// <param name="function">Optional function of the contact</param>
        /// <returns>German letter salutation</returns>
        public override string CreateLetterSalutation(Contact contact, Function function = null)
        {
            var highestTitle = GetHighestAcademicTitle(contact);
            var selectedFunction = GetFunction(function, contact.Gender);
            var salutation = GetSalutation(contact.Gender);

            var sb = new StringBuilder()
                .Append(salutation).Append(" ")
                .AppendIf(selectedFunction, UseIfProvided, selectedFunction)
                .AppendIf(" ", UseIfProvided, selectedFunction)
                .AppendIf(highestTitle, UseIfProvided, highestTitle)
                .AppendIf(" ", UseIfProvided, highestTitle)
                .Append(contact.LastName).Append(" ");

            return sb.ToString().Trim();
        }


        /// <summary>
        /// Retrives the corresponding
        /// string reprentatin of the 
        /// specified function objetc
        /// </summary>
        /// <param name="function">Function to retrieve 
        /// string representation from</param>
        /// <returns>String representation of the function</returns>
        private string GetFunction(Function function, Gender gender)
        {
            if (function == null) return null;

            switch(gender)
            {
                case Gender.MALE:
                    return function.MaleOut;
                case Gender.FEMALE:
                    return function.FemaleOut;
                default:
                    return function.DiversOut;
            }
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
            //  if (String.IsNullOrEmpty(contact.Title)) return null;
            if (contact.Title == null || contact.Title.Count==0) return null;
            else
            {
                return contact.Title.FirstOrDefault();
            }
        }


        /// <summary>
        /// Parses the json settings file to
        /// retrieve the specified value
        /// </summary>
        /// <param name="name">Full json key</param>
        /// <returns>Corresponding json value of
        /// the specified key</returns>
        private string GetJsonProperty(string name)
        {
            try
            {
                var path = name.Split(':');

                JToken node = jsonConfig[path[0]];
                for (int index = 1; index < path.Length; index++)
                {
                    node = node[path[index]];
                }

                return node.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
