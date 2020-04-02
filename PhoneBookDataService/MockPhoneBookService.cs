using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneBookInterfaces;
using PhoneBookModels;

namespace PhoneBookDataService
{
    public class MockPhoneBookService : IPhoneBookService
    {
        readonly List<Contact> _contacts;

        public MockPhoneBookService()
        {
            _contacts = new List<Contact>()
            {
                new Contact { Id = Guid.NewGuid().ToString(), Name = "First item", TelephoneNumber="1111111" },
                new Contact { Id = Guid.NewGuid().ToString(), Name = "Second item", TelephoneNumber="22222" },
                new Contact { Id = Guid.NewGuid().ToString(), Name = "Third item", TelephoneNumber="333333" },
                new Contact { Id = Guid.NewGuid().ToString(), Name = "Fourth item", TelephoneNumber="4444444" },
                new Contact { Id = Guid.NewGuid().ToString(), Name = "Fifth item", TelephoneNumber="55555555" },
                new Contact { Id = Guid.NewGuid().ToString(), Name = "Sixth item", TelephoneNumber="66666666" }
            };
        }

        public async Task<bool> AddContactAsync(Contact contact)
        {
            _contacts.Add(contact);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            var oldItem = _contacts.FirstOrDefault(arg => arg.Id == contact.Id);
            _contacts.Remove(oldItem);
            _contacts.Add(contact);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteContactAsync(string id)
        {
            var oldItem = _contacts.FirstOrDefault(arg => arg.Id == id);
            _contacts.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Contact> GetContactAsync(string id)
        {
            return await Task.FromResult(_contacts.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Contact>> GetIContactAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_contacts);
        }
    }
}
