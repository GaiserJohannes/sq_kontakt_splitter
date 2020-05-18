using KontaktSplitter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KontaktSplitter.Services
{
    public class CRMMockConnector : ICRMConnector
    {
        public bool ContainsContact(Contact contact)
        {
            MessageBox.Show("Der Kontaktsplitter ist noch nicht ans CRM-System angeschlossen", "Achtung");
            return false;
        }

        public void StoreContact(Contact contact)
        {
            MessageBox.Show("Der Kontaktsplitter ist noch nicht ans CRM-System angeschlossen", "Achtung");
        }
    }
}
