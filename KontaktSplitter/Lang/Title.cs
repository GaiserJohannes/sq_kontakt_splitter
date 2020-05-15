using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KontaktSplitter.Lang
{
    public class Title
    {
        public Title() { }

        /// <summary>
        /// name of the title
        /// e.g. doctor
        /// </summary>
        [JsonPropertyName("title")]
        public string Name { get; set; }

        /// <summary>
        /// different types of the same title
        /// e.g. doctor of philosophy
        /// </summary>
        [JsonPropertyName("types")]
        public IList<string> Types { get; set; }

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

        /// <summary>
        /// rank of the title compared to others
        /// </summary>
        [JsonPropertyName("rank")]
        public int Rank { get; set; }
    }
}
