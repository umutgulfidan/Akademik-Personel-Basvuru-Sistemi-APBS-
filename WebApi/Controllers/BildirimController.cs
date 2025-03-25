using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Core.Extensions.Claims;
using Entities.Dtos.Bildirim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using WebApi.Hubs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BildirimController : ControllerBase
    {
        private readonly IBildirimService _bildirimService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IMapper _mapper; // AutoMapper kullanımı

        public BildirimController(IBildirimService bildirimService, IHubContext<NotificationHub> hubContext, IMapper mapper)
        {
            _bildirimService = bildirimService;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        // Admin'in bir kullanıcıya bildirim göndermesi için endpoint
        [HttpPost("SendNotification")]
        public async Task<IActionResult> SendNotification([FromBody] AddBildirimDto addBildirimDto)
        {
            var result = await _bildirimService.AddAdmin(addBildirimDto);
            // Bildirim içeriğini JSON nesnesi olarak gönderiyoruz

            if (result.IsSuccess)
            {
                // DTO'yu mapleyerek SignalR üzerinden gönder
                var notificationDto = _mapper.Map<BildirimDto>(addBildirimDto);

                await _hubContext.Clients.User(addBildirimDto.KullaniciId.ToString())
                    .SendAsync("ReceiveNotification", notificationDto);
                return Ok(result); 
            }

            return BadRequest(result);
        }

        [HttpPut("UpdateNotification")]
        public async Task<IActionResult> UpdateNotification([FromBody] UpdateBildirimDto updateBildirimDto)
        {

            var result = await _bildirimService.UpdateAdmin(updateBildirimDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("DeleteNotification")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var result = await _bildirimService.DeleteAdmin(id); 

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllNotifications()
        {
            var result = await _bildirimService.GetAllForAdmin();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetMyNotifications")]
        public async Task<IActionResult> GetMyNotifications()
        {
            // Kullanıcının giriş yapıp yapmadığını kontrol et
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized(Messages.Unauthorized);
            }

            // Kullanıcı ID'sini JWT'den al
            var userId = User.ClaimUserId();

            if (userId == 0)
            {
                return BadRequest(Messages.UserNotFound);
            }

            // Kullanıcının kendi bildirimlerini getir
            var result = await _bildirimService.GetAllByUser(userId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }

        [HttpPut("MarkAsRead")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            if (User?.Identity?.IsAuthenticated != true)
                return Unauthorized("Bu işlemi gerçekleştirmek için giriş yapmalısınız.");

            var userId = User.ClaimUserId();
            var result = await _bildirimService.MarkAsRead(id,userId);

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("MarkAsUnread")]
        [AuthenticatedOperation]
        public async Task<IActionResult> MarkAsUnread(int id)
        {
            var userId = User.ClaimUserId();
            var result = await _bildirimService.MarkAsUnread(id, userId);

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("DeleteMyNotification")]
        [AuthenticatedOperation]
        public async Task<IActionResult> DeleteMyNotification(int id)
        {
            var userId = User.ClaimUserId();
            var result = await _bildirimService.DeleteByUser(id,userId);

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
    }
}
