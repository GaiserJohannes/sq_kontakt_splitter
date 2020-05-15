using KontaktSplitter.Lang;
using KontaktSplitter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KontaktSplitter.Services
{
    public class DefaultContactSplitter : IContactSplitter
    {
        private IConfiguration configuration = new JSONConfiguration();
        private List<Language> languages = new List<Language>();

        public DefaultContactSplitter()
        {
            var german = new German();

            german.Titles.Add("Professor");
            german.Titles.Add("Prof.");
            german.Titles.Add("Dr.");
            german.Titles.Add("Dipl.");
            german.Titles.Add("rer. nat.");
            german.Titles.Add("med.");
            german.Titles.Add("h.c. mult.");
            german.Titles.Add("Ing.");

            german.Salutations.Add("Frau", Gender.FEMALE);
            german.Salutations.Add("Fr.", Gender.FEMALE);
            german.Salutations.Add("Herr", Gender.MALE);
            german.Salutations.Add("Hr.", Gender.MALE);
            languages.Add(german);

            var english = new English();
            english.Titles.Add("Professor");

            english.Salutations.Add("Mr.", Gender.MALE);
            english.Salutations.Add("Mrs.", Gender.FEMALE);
            english.Salutations.Add("Ms.", Gender.FEMALE);
            languages.Add(english);

        }

        public Contact SplitContact(string input)
        {
            //var languages = configuration.GetLanguages();
            var contacts = new List<Contact>();
            var contact = new Contact();
            languages.ForEach(l => contacts.Add(convert(input, l)));

            return BestMatch(contacts);
        }

        private Contact convert(string input, Language language)
        {
            Contact contact = new Contact();
            contact.Language = language;

            var salutationregex = CreateSalutationRegex(language);
            var salutation = salutationregex.Match(input);

            if (salutation.Success)
            {
                contact.Salutation = salutation.Value;
                contact.Gender = language.Salutations[salutation.Value];
            }

            var titleregex = CreateTitleRegex(language);
            var title = titleregex.Match(input);
            
            while (title.Success)
            {
                contact.Title = string.Format($"{contact.Title} {title.Value}").Trim(); 
                var next = title.NextMatch();
                if (!next.Success)
                {
                    break;
                }
                title = next;
            }

            int nameindex = title.Success ? title.Index + title.Length : salutation.Success ? salutation.Index + salutation.Length : 0;
            var fullname = input.Substring(nameindex).Trim();

            //von lastname, name
            if (fullname.Contains(","))
            {
                int index = fullname.IndexOf(',');
                contact.LastName = fullname.Substring(0, index).Trim();
                contact.Name = fullname.Substring(index + 1).Trim();
            }
            //name von lastname
            else
            {
                var names = fullname.Split(' ');
                if (names.Count() > 1)
                {
                    contact.Name = names[0];
                    contact.LastName = string.Join(" ", names.Skip(1));
                }
                else if (names.Count() == 1)
                {
                    contact.LastName = names[0];
                }
            }
            return contact;
        }

        private Contact BestMatch(List<Contact> contacts)
        {
            var best = contacts.First();
            foreach(var contact in contacts)
            {
                best = Comapare(best, contact);
            }
            return best;
        }

        private Contact Comapare(Contact a, Contact b)
        {
            int aProps = 0;
            int bProps = 0;
            foreach (var info in typeof(Contact).GetProperties().Where(p => p.CanWrite))
            {
                aProps += string.IsNullOrEmpty(info.GetValue(a) as string) ? 0 : 1;
                bProps += string.IsNullOrEmpty(info.GetValue(b) as string) ? 0 : 1;
            }
            return aProps >= bProps ? a : b;
        }

        public Regex CreateSalutationRegex(Language language)
        {
            return new Regex($"({string.Join("|", language.Salutations.Keys.Select(s => Regex.Escape(s)))})", RegexOptions.Compiled);
        }

        public Regex CreateTitleRegex(Language language)
        {
            return new Regex($"({string.Join("|", language.Titles.Select(t => Regex.Escape(t)))})", RegexOptions.Compiled);
        }

    }
}
