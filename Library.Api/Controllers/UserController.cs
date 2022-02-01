using Library.Api.Models.ViewModels;
using Library.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected ResponseViewModel _response;
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this._response = new ResponseViewModel();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<UserViewModel> user = await _userRepository.GetUsers();
                _response.Result = user;
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
                UserViewModel user = await _userRepository.GetUserById(id);
                _response.Result = user;
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
        public async Task<object> Post([FromBody] UserViewModel user)
        {
            try
            {
                UserViewModel model = await _userRepository.CreateUser(user);
                _response.Result = model;
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
        public async Task<object> Put([FromBody] UserViewModel user, int userId)
        {
            try
            {
                UserViewModel model = await _userRepository.UpdateUser(user, userId);
                _response.Result = model;
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
                bool isSuccess = await _userRepository.DeleteUser(id);
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

        [HttpGet("getTopOverdueUsers")]
        public async Task<object> GetUsersWithOverdue()
        {
            try
            {
                IEnumerable<UserViewModelWithOverdue> user = await _userRepository.TopUsersByOverdueTime();
                _response.Result = user;
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
