using KontaktSplitter.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktSplitter.Model
{
    public class Contact
    {
        public string Salutaiton { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Language Language { get; set; }
        public string LetterSalutation { get => Language?.CreateLetterSalutation(this); }
    }
}
