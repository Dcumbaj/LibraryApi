using Library.Api.Models.ViewModels;

namespace Library.Api.Repository
{
    public interface IBookRepository
    {
        Task<List<RentHistoryViewModel>> GetBookRentHistory(int bookId);
    }
}
