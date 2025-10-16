using _BE.Models;
using _BE.Models.Responses;
using _BE.Repositories.Interface;
using _BE.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = _authService.Login(request.Email, request.Password);

            if (token == null)
                return Unauthorized(new APIResponse<string>
                {
                    StatusCode = 401,
                    Message = "Invalid email or password",
                    Data = null
                });

            return Ok(new APIResponse<string>
            {
                StatusCode = 200,
                Message = "Login successful",
                Data = token
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
