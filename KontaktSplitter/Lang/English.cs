using KontaktSplitter.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace KontaktSplitter.Lang
{
    public class English : Language
    {

        /*Object representation of the json file*/
        private JObject jsonConfig;

        public English()
        {
            /*Load language settings from the configuraton file*/
            Name = "english";

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

        public override string CreateLetterSalutation(Contact contact, Function function = null)
        {
            throw new NotImplementedException();
        }
    }
}
