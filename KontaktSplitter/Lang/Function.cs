using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KontaktSplitter.Lang
{
    public class Function
    {
        public Function() { }

        /// <summary>
        /// name of the function
        /// e.g. Duke
        /// </summary>
        [JsonPropertyName("function")]
        public string Name { get; set; }

        /// <summary>
        /// like the title should be printet for male
        /// </summary>
        [JsonPropertyName("m")]
        public string MaleOut { get; set; }

        /// <summary>
        /// like the title should be printet for female
        /// </summary>
        [JsonPropertyName("w")]
        public string FemaleOut { get; set; }

        /// <summary>
        /// like the title should be printet for divers
        /// </summary>
        [JsonPropertyName("d")]
        public string DiversOut { get; set; }
    }
}
