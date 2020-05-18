using KontaktSplitter.Lang;
using System.Collections.ObjectModel;

namespace KontaktSplitter.Model
{
    public class Contact
    {
        public string Salutation { get; set; }
        public ObservableCollection<string> Title { get; set; } = new ObservableCollection<string>();
        public string Name { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Language Language { get; set; }
        public string LetterSalutation { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Contact contact))
            {
                return false;
            }

            if (!(Salutation != null && contact.Salutation != null && Salutation.Equals(contact.Salutation) || Salutation == null && contact.Salutation == null))
            {
                return false;
            }
            if (!(Title != null && contact.Title != null && Title.Count == contact.Title.Count))
            {
                return false;
            }
            for(int i = 0; i < Title.Count; i++)
            {
                if (! Title[i].Equals(contact.Title[i]))
                {
                    return false;
                }
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
