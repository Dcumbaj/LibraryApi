using Library.Api.Models.ViewModels;

namespace Library.Api.Repository
{
    public interface IUserContactRepository
    {
        Task<IEnumerable<UserContactViewModel>> GetUserContacts();
        Task<UserContactViewModel> GetUserContactById(int userContactId);
        Task<UserContactViewModel> CreateUserContact(UserContactViewModel userContact);
        Task<UserContactViewModel> UpdateUserContact(UserContactViewModel userContact, int userContactId);
        Task<bool> DeleteUserContact(int userContactId);
    }
}
