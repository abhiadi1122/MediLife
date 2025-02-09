using MediLife.API.Framework;
using MediLife.BusinessProvider.IProviders;
using MediLife.DataObjects;
using Microsoft.AspNetCore.Mvc;

namespace MediLife.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : MediLifeControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            try
            {
                _userService.RegisterUser(user);
                return Ok(new { message = "User registered successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getUserByEmail")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest user)
        {
            var token = _userService.Authenticate(user);

            if (token == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new { token });
        }
    }
}
