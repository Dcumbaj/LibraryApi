using Library.Api.Models.ViewModels;
using Library.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContactController : ControllerBase
    {
        protected ResponseViewModel _response;
        private IUserContactRepository _userContactRepository;

        public UserContactController(IUserContactRepository userContactRepository)
        {
            _userContactRepository = userContactRepository;
            this._response = new ResponseViewModel();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<UserContactViewModel> userContact = await _userContactRepository.GetUserContacts();
                _response.Result = userContact;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {

            try
            {
                UserContactViewModel userContact = await _userContactRepository.GetUserContactById(id);
                _response.Result = userContact;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]
        public async Task<object> Post([FromBody] UserContactViewModel userContact)
        {
            try
            {
                UserContactViewModel model = await _userContactRepository.CreateUserContact(userContact);
                if (model == null)
                {
                    _response.Result = "The user with id:" + userContact.UserId + " does not exist";
                }
                else
                {
                    _response.Result = model;
                }
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPut]
        public async Task<object> Put([FromBody] UserContactViewModel userContact, int userContactId)
        {
            try
            {
                UserContactViewModel model = await _userContactRepository.UpdateUserContact(userContact, userContactId);
                if (model == null)
                {
                    _response.Result = "The user with id:" + userContact.UserId + " does not exist";
                }
                else
                {
                    _response.Result = model;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _userContactRepository.DeleteUserContact(id);
                _response.Result = isSuccess;
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
