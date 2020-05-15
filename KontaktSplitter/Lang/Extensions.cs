using System;
using System.Text;

namespace KontaktSplitter.Lang
{
    /// <summary>
    /// Namespace providing extension for the existing 
    /// base class library
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Appends a the specified string to an
        /// existing StringBuilder class if the given
        /// predicate is verified.
        /// </summary>
        /// <param name="sb">StringBuilder object building
        /// the appended string</param>
        /// <param name="value">String to be added</param>
        /// <param name="predicate">Predicate to be verified</param>
        /// <param name="param">Additional parameter used as 
        /// argument of the predicate</param>
        /// <returns>Returns the updated StringBuilder object</returns>
        public static StringBuilder AppendIf(this StringBuilder sb, string value, Predicate<string> predicate, string param)
        {
            if (predicate(param)) sb.Append(value);
            return sb;
        }
    }
}
