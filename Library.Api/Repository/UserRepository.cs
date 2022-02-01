﻿using Library.Api.DbContexts;
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
                DateOfBirth = user.DateOfBirth,
                Contacts = user.Contacts
            };

            if (_user.Contacts != null)
            {
                foreach (var contact in _user.Contacts)
                {
                    if (_db.UserContacts.FirstOrDefault(x => x.Id == contact.Id) == null)
                    {
                        _db.UserContacts.Add(contact);
                    }
                }
            }

            _db.Users.Add(_user);
            
            await  _db.SaveChangesAsync();

            return user;
        }

        public async Task<UserViewModel> GetUserById(int userId)
        {
            User user = await _db.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            UserViewModel viewModel = new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                Contacts = user.Contacts
            };

            return viewModel;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            List<User> userList = await _db.Users.Include(x => x.Contacts).ToListAsync();
            List<UserViewModel> _userList = new List<UserViewModel>();

            foreach (var user in userList)
            {
                _userList.Add(new UserViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Contacts = user.Contacts
                });
            }
            
            return _userList;
        }

        public async Task<UserUpdateViewModel> UpdateUser(UserUpdateViewModel user, int userId)
        {

            var _user = await _db.Users.FirstOrDefaultAsync(n => n.Id == userId);

            if (_user != null)
            {
                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;
                _user.Gender = user.Gender;
                _user.DateOfBirth = user.DateOfBirth;

                _db.Users.Update(_user);

                await _db.SaveChangesAsync();
            }

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

        public async Task<IEnumerable<UserViewModelWithOverdue>> TopUsersByOverdueTime()
        {
            List<User> userList = await _db.Users.Include(x => x.RentHistories).ToListAsync();
            List<UserViewModelWithOverdue> _userList = new List<UserViewModelWithOverdue>();

            foreach (var user in userList)
            {
                foreach (var rent in user.RentHistories.Where(x => x.DueDate < DateTime.Today && x.RentStatusId == (int)RentStatusType.NotReturned))
                {
                    user.TotalOvedueInDays += (DateTime.Today - rent.RentDate).Days;
                }
            }

            foreach (var user in userList)
            {
                _userList.Add(new UserViewModelWithOverdue()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Contacts = user.Contacts,
                    TotalOvedueInDays = user.TotalOvedueInDays
                });
            }

            return _userList.Where(x => x.TotalOvedueInDays > 0);
        }
    }
}
