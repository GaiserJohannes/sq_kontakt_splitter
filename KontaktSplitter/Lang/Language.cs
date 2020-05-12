using KontaktSplitter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktSplitter.Lang
{
    public abstract class Language
    {
        public List<string> Titles { get; } = new List<string>();
        public List<string> Functions { get; } = new List<string>();
        public Dictionary<string, Gender> Salutaions { get; } = new Dictionary<string, Gender>();

        public abstract string CreateLetterSalutation(Contact contact, string function = null);
    }
}
