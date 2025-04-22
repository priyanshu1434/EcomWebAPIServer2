using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EcomWebAPIServer2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var authResult = _authService.Authentication(request.Email, request.Password);
            if (authResult == null)
            {
                return Unauthorized("Invalid username or password");
            }
            return Ok(new { token = authResult.Token, userId = authResult.UserId, role = authResult.Role });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
