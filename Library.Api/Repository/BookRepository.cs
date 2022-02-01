using Library.Api.DbContexts;
using Library.Api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<RentHistoryViewModel>> GetBookRentHistory(int bookId)
        {
            var book = await _db.Books.Include(x => x.RentHistories).FirstOrDefaultAsync(x => x.Id == bookId);
            if (book == null)
            {
                return null;
            }

            List<RentHistoryViewModel> _rentHistory = new List<RentHistoryViewModel>();

            foreach (var history in book.RentHistories)
            {
                _rentHistory.Add(new RentHistoryViewModel()
                {
                    RentDate = history.RentDate,
                    DueDate = history.DueDate,
                    BookId = history.BookId,
                    UserId = history.UserId,
                    RentStatusId = history.RentStatusId
                });

            }

            return _rentHistory;
        }
    }
}
