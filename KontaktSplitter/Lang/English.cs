using KontaktSplitter.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KontaktSplitter.Lang
{
    public class English : Language
    {

        /// <summary>
        /// Langauge specific settings
        /// </summary>
        private readonly IConfigurationSection langConfig;

        /*Object representation of the json file*/
        private JObject jsonConfig;

        public English()
        {
            /*Load language settings from the configuraton file*/
            Name = "english";
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

                Titles = JsonConvert.DeserializeObject<HashSet<string>>(GetJsonProperty($"languages:{Name}:titles"));
                Functions = JsonConvert.DeserializeObject<List<Function>>(GetJsonProperty($"languages:{Name}:functions"));
                Salutations = JsonConvert.DeserializeObject<Dictionary<string, Gender>>(GetJsonProperty($"languages:{Name}:salutations"));
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
            return !string.IsNullOrEmpty(parameter);
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

            switch (gender)
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
        /// Retrieves the highest academic title
        /// if the specified contact
        /// </summary>
        /// <param name="contact">Contact to retrieve
        /// its highest academic title for</param>
        /// <returns>String representing the contacts
        /// highest academic title</returns>
        private string GetHighestAcademicTitle(Contact contact)
        {
            if (contact.Title == null || contact.Title.Count == 0) return null;
            else
            {
                return contact.Title.FirstOrDefault();
            }
        }

        /// <summary>
        /// Creates a english letter salutation string for the provided contact
        /// object
        /// </summary>
        /// <param name="contact">Contact to build a sulation string for</param>
        /// <param name="function">Optional function of the contact</param>
        /// <returns>English letter salutation</returns>
        public override string CreateLetterSalutation(Contact contact, Function function = null)
        {
            var highestTitle = GetHighestAcademicTitle(contact);
            var selectedFunction = GetFunction(function, contact.Gender);
            var salutation = langConfig["general"];

            var sb = new StringBuilder()
                .Append(salutation).Append(" ");
            if (UseIfProvided(contact.Salutation))
            {
                if (UseIfProvided(selectedFunction))
                {
                    sb.Append(selectedFunction).Append(" ");
                }
                else
                {
                    sb.Append(contact.Salutation).Append(" ")
                        .AppendIf(highestTitle, UseIfProvided, highestTitle)
                        .AppendIf(" ", UseIfProvided, highestTitle)
                        .Append(contact.LastName).Append(" ");
                }
            }
            else
            {
                sb.Append(langConfig["default"]).Append(" ");
            }

            return sb.ToString().Trim();
        }
    }
}
