using Business.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Entities.Dtos.Email;
using Business.Constants;
using Core.Extensions.Claims;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs; // Bu dosyayı oluşturmanız gerekecek

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public EmailController(IEmailService emailService, IHubContext<NotificationHub> hubContext)
        {
            _emailService = emailService;
            _hubContext = hubContext;
        }

        private bool HasEmailPermission()
        {
            if (User?.Identity?.IsAuthenticated != true)
                return false;

            var roles = User.ClaimRoles();
            return roles.Contains("Admin") || roles.Contains("Yonetici");
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendEmail(SendEmailDto sendEmailDto)
        {
            if (!HasEmailPermission())
            {
                return Unauthorized();
            }

            var result = await _emailService.SendEmail(sendEmailDto.Email, sendEmailDto.Subject, sendEmailDto.Body);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("SendToUser")]
        public async Task<IActionResult> SendEmailToUser(SendUserEmailDto sendUserEmailDto)
        {
            if (!HasEmailPermission())
            {
                return Unauthorized();
            }

            var result = await _emailService.SendEmailToUser(sendUserEmailDto.UserId, sendUserEmailDto.Subject, sendUserEmailDto.Body);

            if (result.IsSuccess)
            {
                var connectionId = NotificationHub.GetConnectionId(sendUserEmailDto.UserId.ToString()); // Hub üzerinden bağlantı ID'sini al

                if (connectionId != null)
                {
                    var bildirim = NotificationTemplates.Info("Yeni bir e-postanız var!", $"Gelen kutunuza '{sendUserEmailDto.Subject}' başlıklı yeni bir e-posta iletildi. Detaylar için e-mailinizi kontrol ediniz.");
                    // Kullanıcı aktifse, ona bildirim gönder
                    await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", bildirim);
                }
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}