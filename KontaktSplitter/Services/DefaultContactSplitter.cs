using KontaktSplitter.Lang;
using KontaktSplitter.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace KontaktSplitter.Services
{
    public class DefaultContactSplitter : IContactSplitter
    {
        private List<Language> languages = new List<Language>();

        /// <summary>
        /// initialize available languages: German and English
        /// </summary>
        public DefaultContactSplitter()
        {
            var german = new German();
            languages.Add(german);

            var english = new English();
            languages.Add(english);

        }

        /// <summary>
        /// splitt input string into contacts and return contact with most filled properties
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Contact SplitContact(string input)
        {
            var contacts = new List<Contact>();
            languages.ForEach(l => contacts.Add(SplitLanguageToContact(input, l)));


            return contacts.OrderByDescending(c => FilledProperties(c)).First();
        }

        /// <summary>
        /// split input string to a contact of the given language
        /// </summary>
        /// <param name="input"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private Contact SplitLanguageToContact(string input, Language language)
        {
            Contact contact = new Contact();
            contact.Language = language;

            //find salutation
            var salutationregex = CreateSalutationRegex(language);
            var salutation = salutationregex.Match(input);
            if (salutation.Success)
            {
                contact.Salutation = salutation.Value;
                contact.Gender = language.Salutations[salutation.Value];
            }

            //find and order all titles: profesor > dr. > Dipl. Ing -> order in Langsettings.json
            var titleregex = CreateTitleRegex(language);
            var title = titleregex.Match(input);
            while (title.Success)
            {
                contact.Title.Add(title.Value);
                var next = title.NextMatch();
                if (!next.Success)
                {
                    break;
                }
                title = next;
            }            
            contact.Title = new ObservableCollection<string>(contact.Title.OrderByDescending(t => language.Titles.IndexOf(t)));

            int nameindex = title.Success ? title.Index + title.Length : salutation.Success ? salutation.Index + salutation.Length : 0;
            var fullname = input.Substring(nameindex).Trim();

            //lastname and name with comma separated: lastname, name
            if (fullname.Contains(","))
            {
                int index = fullname.IndexOf(',');
                contact.LastName = fullname.Substring(0, index).Trim();
                contact.Name = fullname.Substring(index + 1).Trim();
            }
            //normal case: name lastname
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

        /// <summary>
        /// return amount of filled Properties of a Contact
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private int FilledProperties(Contact a)
        {
            int Props = 0;
            foreach (var info in typeof(Contact).GetProperties().Where(p => p.CanWrite))
            {
                Props += string.IsNullOrEmpty(info.GetValue(a) as string) ? 0 : 1;
            }
            return Props;
        }

        /// <summary>
        /// create regex for salutations of a language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public Regex CreateSalutationRegex(Language language)
        {
            return new Regex($"({string.Join("|", language.Salutations.Keys.Select(s => Regex.Escape(s)))})", RegexOptions.Compiled);
        }

        /// <summary>
        /// create regex of titles of a language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public Regex CreateTitleRegex(Language language)
        {
            return new Regex($"({string.Join("|", language.Titles.Select(t => Regex.Escape(t)))})", RegexOptions.Compiled);
        }

    }
}
