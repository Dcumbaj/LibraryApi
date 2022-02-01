using Library.Api.Models.ViewModels;
using Library.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        protected ResponseViewModel _response;
        private IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            this._response = new ResponseViewModel();
        }

        [HttpGet]
        [Route("GetBookRentHistory/{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                List<RentHistoryViewModel> rentHistory = await _bookRepository.GetBookRentHistory(id);
                _response.Result = rentHistory;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
