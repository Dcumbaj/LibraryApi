using Library.Api.Models.ViewModels;

namespace Library.Api.Repository
{
    public interface IUserRepository
    {
        Task<UserViewModel> CreateUser(UserViewModel user);
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<UserViewModel> GetUserById(int userId);
        Task<UserViewModel> UpdateUser(UserViewModel user, int userId);
        Task<bool> DeleteUser(int userId);
        Task<IEnumerable<UserViewModel>> TopUsersByOverdueTime();
    }
}   
