using Library.Api.DbContexts;
using Library.Api.Models;
using Library.Api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Repository
{
    public class UserContactRepository : IUserContactRepository
    {
        private readonly ApplicationDbContext _db;

        public UserContactRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<UserContactViewModel> CreateUserContact(UserContactViewModel userContact)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == userContact.UserId);
            if (user == null)
            {
                return null;
            }
            var _userContact = new UserContact()
            {
                PhoneNumber = userContact.PhoneNumber,
                MobileNumber = userContact.MobileNumber,
                Email = userContact.Email,
                UserId = userContact.UserId
            };

            await _db.UserContacts.AddAsync(_userContact);
            return userContact;
        }

        public async Task<UserContactViewModel> GetUserContactById(int userContactId)
        {
            UserContact userContact = await _db.UserContacts.Where(x => x.Id == userContactId).FirstOrDefaultAsync();

            if (userContact == null)
            {
                return null;
            }

            UserContactViewModel _userContact = new UserContactViewModel()
            {
                PhoneNumber = userContact.PhoneNumber,
                MobileNumber = userContact.MobileNumber,
                Email = userContact.Email,
                UserId = userContact.UserId
            };

            return _userContact;
        }

        public async Task<IEnumerable<UserContactViewModel>> GetUserContacts()
        {
            List<UserContact> userContactList = await _db.UserContacts.ToListAsync();
            List<UserContactViewModel> _userContactList = new List<UserContactViewModel>();

            foreach (var userContact in userContactList)
            {
                _userContactList.Add(new UserContactViewModel()
                {
                    PhoneNumber = userContact.PhoneNumber,
                    MobileNumber = userContact.MobileNumber,
                    Email = userContact.Email,
                    UserId = userContact.UserId
                });
            }

            return _userContactList;
        }

        public async Task<UserContactViewModel> UpdateUserContact(UserContactViewModel userContact, int userId)
        {
            var _userContact = await _db.UserContacts.FirstOrDefaultAsync(n => n.Id == userId);
            bool userExists = await _db.Users.AnyAsync(n => n.Id == userContact.UserId);

            if (_userContact != null && userExists)
            {
                _userContact.PhoneNumber = userContact.PhoneNumber;
                _userContact.MobileNumber = userContact.MobileNumber;
                _userContact.Email = userContact.Email;
                _userContact.UserId = userContact.UserId;

                _db.UserContacts.Update(_userContact);

                await _db.SaveChangesAsync();
            }

            return userContact;
        }

        public async Task<bool> DeleteUserContact(int userContactId)
        {
            try
            {
                UserContact userContact = await _db.UserContacts.FirstOrDefaultAsync(x => x.Id == userContactId);
                if (userContact == null)
                {
                    return false;
                }
                _db.UserContacts.Remove(userContact);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
