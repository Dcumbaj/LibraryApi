using Library.Api.Models.ViewModels;

namespace Library.Api.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<UserViewModel> GetUserById(int userId);
        Task<UserViewModel> CreateUser(UserViewModel user);
        Task<UserViewModel> UpdateUser(UserViewModel user, int userId);
        Task<IEnumerable<UserViewModel>> TopUsersByOverdueTime();
        Task<bool> DeleteUser(int userId);
    }
}   
