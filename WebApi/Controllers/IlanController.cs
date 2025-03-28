using Business.Abstract;
using Business.Abstracts;
using Business.Constants;
using Core.Entities.Concrete;
using Entities.Concretes;
using Entities.Dtos.Ilan;
using Entities.Dtos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlanController : ControllerBase
    {
        IIlanService _ilanService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public IlanController(IIlanService ilanService, IHubContext<NotificationHub> hubContext)
        {
            _ilanService = ilanService;
            _hubContext = hubContext;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ilanService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getilansbyquery")]
        public async Task<IActionResult> GetPaginatedIlans([FromQuery] UserIlanQueryDto query)
        {
            var result = await _ilanService.GetIlansByQuery(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getilansbyqueryforadmin")]
        public async Task<IActionResult> GetPaginatedIlansForAdmin([FromQuery] AdminIlanQueryDto query)
        {
            var result = await _ilanService.GetIlansByQueryForAdmin(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getallactives")]
        public async Task<IActionResult> GetAllActives()
        {
            var result = await _ilanService.GetAllActiveIlans();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallexpireds")]
        public async Task<IActionResult> GetAllExpireds()
        {
            var result = await _ilanService.GetAllExpiredIlans();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ilanService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ilanService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateIlanDto ilan)
        {
            var result = await _ilanService.Update(ilan);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(AddIlanDto ilan)
        {
            var result = await _ilanService.Add(ilan);
            if (result.IsSuccess)
            {
                var bildirim = NotificationTemplates.Info("Yeni ilan eklendi!", $"'{ilan.Baslik}' ismiyle yeni bir ilan oluşturuldu. Anasayfadan göz atabilirsin.");
                // SignalR üzerinden tüm kullanıcılara mesaj gönder
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", bildirim);
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateIlan(int id)
        {
            var result = await _ilanService.ActivateIlan(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateIlan(int id)
        {
            var result = await _ilanService.DeactivateIlan(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
