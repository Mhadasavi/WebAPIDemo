using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;
using WebAPIDemo.Repository.IRepository;

namespace WebAPIDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            var userModel = _userRepository.Authenticate(user.Username, user.Password);
            if (userModel == null)
            {
                return BadRequest(new { message = "Wrong Username or Password" });
            }
            return Ok(userModel);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User userModel)
        {
            bool IsUserExist = _userRepository.isUserExist(userModel.Username);

            if (IsUserExist)
            {
                return BadRequest(new { message = "User already exist" });
            }
            var user = _userRepository.Register(userModel.Username, userModel.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Error while registering" });
            }
            return Ok(user);
        }
    }
}
