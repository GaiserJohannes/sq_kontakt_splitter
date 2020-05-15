using KontaktSplitter.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace KontaktSplitter.Lang
{
    public class English : Language
    {

        public English()
        {
            Titles = config.GetSection("languages:english:titles").Get<List<string>>();
            Functions = config.GetSection("languages:english:functions").Get<List<Function>>();
            Salutations = config.GetSection("languages:english:salutaitons").Get<Dictionary<string, Gender>>();
        }

        public override string CreateLetterSalutation(Contact contact, string function = null)
        {
            throw new NotImplementedException();
        }
    }
}
