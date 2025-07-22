using Microsoft.AspNetCore.Mvc;
using PlannerApp.API.Models;
using PlannerApp.API.Services;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Google.Apis.Auth;

namespace PlannerApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        private readonly IConfiguration _config;

        public AuthController(UserService userService, JwtService jwtService, IConfiguration config)
        {
            _userService = userService;
            _jwtService = jwtService;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _userService.UsernameOrEmailExistsAsync(dto.Name, dto.Email))
                return BadRequest("Username or email already exists.");

            var user = await _userService.RegisterAsync(dto);
            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.ValidateLoginAsync(dto);
            if (user == null) return Unauthorized("Invalid credentials.");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("login/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto dto)
        {
            try
            {
                var googleClientId = _config["Google:ClientId"];
                var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { googleClientId }
                });

                var user = await _userService.GoogleLoginAsync(payload.Subject, payload.Name, payload.Email);
                var token = _jwtService.GenerateToken(user);
                return Ok(new { token });
            }
            catch
            {
                return BadRequest("Invalid Google token.");
            }
        }
    }
}
