using Business.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Entities.Dtos.Email;
using Business.Constants;
using Core.Extensions.Claims; // Bu dosyayı oluşturmanız gerekecek

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        private bool HasEmailPermission()
        {
            if (User?.Identity?.IsAuthenticated != true)
                return false;

            var roles = User.ClaimRoles();
            return roles.Contains("Admin") || roles.Contains("Yonetici");
        }

        [HttpPost("send")]
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

        [HttpPost("send-to-user")]
        public async Task<IActionResult> SendEmailToUser(SendUserEmailDto sendUserEmailDto)
        {
            if (!HasEmailPermission())
            {
                return Unauthorized();
            }

            var result = await _emailService.SendEmailToUser(sendUserEmailDto.UserId, sendUserEmailDto.Subject, sendUserEmailDto.Body);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}