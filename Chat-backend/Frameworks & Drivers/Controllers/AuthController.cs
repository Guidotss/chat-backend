using Chat_backend.Interfaces;
using Chat_backend.Interfaces.Dtos;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat_backend.Frameworks___Drivers.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        private IActionResult HandleErrors(Exception ex)
        {
            return ex switch
            {
                _ => StatusCode(500, new { ok = false, message = "Internal server errror" })
            };
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] NewUserDto newUser)
        {
            try
            {
                var user = await _userRepository.CreateUser(newUser);
                return Ok(new { ok = true, user });

            }
            catch (Exception ex)
            {
                return HandleErrors(ex);
            }
        }
    }
}
