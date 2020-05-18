using KontaktSplitter.Model;

namespace KontaktSplitter.Services
{
    public interface ICRMConnector
    {
        void StoreContact(Contact contact);
        bool ContainsContact(Contact contact);
    }
}
