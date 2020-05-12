using KontaktSplitter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktSplitter.Services
{
    public class CRMMockConnector : ICRMConnector
    {
        public bool ContainsContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void StoreContact(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
