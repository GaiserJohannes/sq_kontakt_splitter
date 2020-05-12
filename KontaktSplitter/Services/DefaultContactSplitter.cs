using KontaktSplitter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktSplitter.Services
{
    public class DefaultContactSplitter : IContactSplitter
    {
        private IConfiguration configuration = new JSONConfiguration();

        public Contact SplitContact(string input)
        {

            var elements = input.Split(' ');
            var languages = configuration.GetLanguages();
            var contact = new Contact();

            foreach (var lang in languages)
            {
                foreach (var element in elements)
                {
                    if (lang.Salutaions.ContainsKey(element))
                    {
                        contact.Language = lang;
                        contact.Gender = lang.Salutaions[element];
                        contact.Salutaiton = element;
                    }
                }
            }

            contact.Name = elements[elements.Length - 1];
            contact.LastName = elements.Last();






            return contact;
        }
    }
}
