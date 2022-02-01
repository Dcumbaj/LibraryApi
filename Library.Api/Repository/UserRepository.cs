using Library.Api.DbContexts;
using Library.Api.Models;
using Library.Api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<UserViewModel> CreateUser(UserViewModel user)
        {
            var _user = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Contacts = user.Contacts
            };

            foreach (var contact in _user.Contacts)
            {
                if (_db.UserContacts.FirstOrDefault(x => x.Id == contact.Id) == null)
                {
                    _db.UserContacts.Add(contact);
                }
            }

            _db.Users.Add(_user);
            
            await  _db.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                User user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if (user == null)
                {
                    return false;
                }
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<UserViewModel> GetUserById(int userId)
        {
            User user = await _db.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            UserViewModel viewModel = new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Contacts = user.Contacts
            };

            return viewModel;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            List<User> userList = await _db.Users.ToListAsync();
            List<UserViewModel> _userList = new List<UserViewModel>();

            foreach (var user in userList)
            {
                _userList.Add(new UserViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    Contacts = user.Contacts
                });
            }

            return _userList;
        }

        public async Task<IEnumerable<UserViewModel>> TopUsersByOverdueTime()
        {
            List<User> userList = await _db.Users.ToListAsync();
            List<UserViewModel> _userList = new List<UserViewModel>();

            foreach (var user in userList)
            {
                foreach (var rent in user.RentHistories.Where(x => x.DueDate < DateTime.Today))
                {
                    user.TotalOvedueInDays += (DateTime.Today - rent.RentDate).Days;
                }

                _userList.Add(new UserViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    Contacts = user.Contacts,
                    TotalOvedueInDays = user.TotalOvedueInDays
                }) ;
            }

            return _userList.Where(x => x.TotalOvedueInDays > 0);
        }

        public async Task<UserViewModel> UpdateUser(UserViewModel user, int userId)
        {

            var _user = _db.Users.FirstOrDefault(n => n.Id == userId);

            if (user != null)
            {
                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;
                _user.Gender = user.Gender;
                _user.Contacts = user.Contacts;

                _db.Users.Update(_user);

                await _db.SaveChangesAsync();
            }

            return user;
        }
    }
}
