using Business.Abstract;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Core.Extensions.Claims;
using Entities.Dtos.Pozisyon;
using Entities.Dtos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public UserController(IUserService userService, IHubContext<NotificationHub> hubContext)
        {
            this._userService = userService;
            this._hubContext = hubContext;
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetUserDto(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getusersbyquery")]
        public async Task<IActionResult> GetPaginatedUsers([FromQuery] UserQueryDto query)
        {
            var result = await _userService.GetUsersByQuery(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        // Kullanıcıyı aktif hale getiren endpoint
        [HttpPut("activate/{userId}")]
        public async Task<IActionResult> ActivateUser(int userId)
        {
            var result = await _userService.ActivateUserAsync(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Kullanıcıyı devre dışı bırakan endpoint
        [HttpPut("deactivate/{userId}")]
        public async Task<IActionResult> DeactivateUser(int userId)
        {
            var result = await _userService.DeactivateUserAsync(userId);
            if (result.IsSuccess)
            {
                // SignalR üzerinden tüm kullanıcılara mesaj gönder
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"Kullanıcı {userId} yasaklandı!");
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody]UpdateUserInfoDto updateUserDto)
        {
            // Kullanıcının giriş yapıp yapmadığını kontrol et
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized(Messages.Unauthorized);
            }
            var userId = User.ClaimUserId();
            var result = await _userService.UpdateProfileAsync(userId,updateUserDto);
            return Ok(result);
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordDto changePasswordDto)
        {
            // Kullanıcının giriş yapıp yapmadığını kontrol et
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized(Messages.Unauthorized);
            }
            var userId = User.ClaimUserId();
            var result = _userService.ChangePassword(changePasswordDto);
        }

    }
}
