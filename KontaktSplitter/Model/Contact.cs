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
        public string Salutation { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Language Language { get; set; }
        public string LetterSalutation { get => Language?.CreateLetterSalutation(this); }

        public override bool Equals(object obj)
        {
            var contact = obj as Contact;
            if(contact == null)
            {
                return false;
            }

            if (!(Salutation != null && contact.Salutation != null && Salutation.Equals(contact.Salutation) || Salutation == null && contact.Salutation == null))
            {
                return false;
            }
            if (!(Title != null && contact.Title != null && Title.Equals(contact.Title) || Title == null && contact.Title == null))
            {
                return false;
            }
            if (!(Name != null && contact.Name != null && Name.Equals(contact.Name) || Name == null && contact.Name == null))
            {
                return false;
            }
            if (!(LastName != null && contact.LastName != null && LastName.Equals(contact.LastName) || LastName == null && contact.LastName == null))
            {
                return false;
            }
            if (Gender != contact.Gender)
            {
                return false;
            }
            if (!(Language != null && contact.Language != null && Language.GetType().Name.Equals(contact.Language.GetType().Name) || Language == null && contact.Language == null))
            {
                return false;
            }
            return true;
        }
    }
}
