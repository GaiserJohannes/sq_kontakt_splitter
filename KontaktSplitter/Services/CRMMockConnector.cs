using KontaktSplitter.Model;
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
