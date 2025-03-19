using Business.Abstract;
using Business.Abstracts;
using Entities.Dtos.Pozisyon;
using Entities.Dtos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
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
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
