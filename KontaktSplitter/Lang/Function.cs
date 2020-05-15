namespace KontaktSplitter.Lang
{
    public class Function
    {
        /// <summary>
        /// name of the function
        /// e.g. Duke
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// like the title should be printet for male
        /// </summary>
        public string MaleOut { get; set; }

        /// <summary>
        /// like the title should be printet for female
        /// </summary>
        public string FemaleOut { get; set; }

        /// <summary>
        /// like the title should be printet for divers
        /// </summary>
        public string DiversOut { get; set; }
    }
}
