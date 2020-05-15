using KontaktSplitter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktSplitter.Services
{
    public interface IContactSplitter
    {
        Contact SplitContact(string input);
    }
}
