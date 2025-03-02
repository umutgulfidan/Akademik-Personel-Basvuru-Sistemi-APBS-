using Business.Abstract;
using Business.BusinessAspects;
using Business.Dtos;
using Core.Extensions.Claims;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        // Kullanıcı giriş işlemi
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var loginResult = await _authService.LoginAsync(userForLoginDto);
            if (!loginResult.IsSuccess)
            {
                return BadRequest(loginResult.Message);
            }

            var tokenResult = await _authService.CreateAccessTokenAsync(loginResult.Data);
            if (tokenResult.IsSuccess)
            {
                return Ok(tokenResult);
            }
            return BadRequest(tokenResult.Message);
        }

        // Kullanıcı kayıt işlemi
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            var registerResult = await _authService.RegisterAsync(userForRegisterDto);
            if (!registerResult.IsSuccess)
            {
                return BadRequest(registerResult.Message);
            }

            var tokenResult = await _authService.CreateAccessTokenAsync(registerResult.Data);
            if (tokenResult.IsSuccess)
            {
                return Ok(tokenResult);
            }
            return BadRequest(tokenResult.Message);
        }

        [HttpGet("getuserbytoken")]
        public async Task<IActionResult> GetUserFromToken()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = HttpContext.User.ClaimUserId();

            if (userId == null)
            {
                return Unauthorized("Invalid or missing user ID in token.");
            }

            var result = await _userService.GetUserDto(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

    }
}
