using Library.Api.Models.ViewModels;

namespace Library.Api.Repository
{
    public interface IUserContactRepository
    {
        Task<IEnumerable<UserContactViewModel>> GetUserContacts();
        Task<UserContactViewModel> GetUserContactById(int userContactId);
        Task<UserContactViewModel> CreateUserContact(UserContactViewModel user);
        Task<UserContactViewModel> UpdateUserContact(UserContactViewModel user, int userId);
        Task<bool> DeleteUserContact(int userId);
    }
}
