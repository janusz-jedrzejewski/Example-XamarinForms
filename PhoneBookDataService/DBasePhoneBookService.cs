using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBookInterfaces;
using PhoneBookModels;

namespace PhoneBookDataService
{
    public class DBasePhoneBookService : IPhoneBookService
    {
        public DBasePhoneBookService()
        {
            
        }
        public Task<bool> AddContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteContactAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> GetContactAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetIContactAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }
    }
}
