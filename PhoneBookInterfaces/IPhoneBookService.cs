using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBookModels;

namespace PhoneBookInterfaces
{
    public interface IPhoneBookService
    {
        Task<bool> AddContactAsync(Contact contact);
        Task<bool> UpdateContactAsync(Contact contact);
        Task<bool> DeleteContactAsync(string id);
        Task<Contact> GetContactAsync(string id);
        Task<IEnumerable<Contact>> GetContactsAsync(bool forceRefresh = false);
    }
}
