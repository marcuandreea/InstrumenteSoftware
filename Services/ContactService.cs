using Microsoft.EntityFrameworkCore;
using mvc.IRepository;
using mvc.Models;

namespace mvc.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task AddContactAsync(string userID, string message, DateTime timesent)
        {
            var contact = new Contact
            {
                userID = userID,
                message = message,
                timeSent = timesent
            };
            await _contactRepository.AddContactAsync(contact);
        }

        public async Task<IEnumerable<Contact>> GetUserContactsAsync(string userID)
        {
            return await _contactRepository.GetContactsByUserIdAsync(userID);
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            await _contactRepository.UpdateAsync(contact);
        }

        public async Task DeleteAsync(int id)
        {
            await _contactRepository.DeleteAsync(id);
        }
    }

}
