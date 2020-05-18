using KontaktSplitter.Model;

namespace KontaktSplitter.Services
{
    public interface IContactSplitter
    {
        Contact SplitContact(string input);
    }
}
