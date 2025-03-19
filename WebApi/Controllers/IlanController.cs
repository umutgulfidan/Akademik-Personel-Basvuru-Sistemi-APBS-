using Business.Abstract;
using Business.Abstracts;
using Entities.Concretes;
using Entities.Dtos.Ilan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlanController : ControllerBase
    {
        IIlanService _ilanService;

        public IlanController(IIlanService ilanService)
        {
            _ilanService = ilanService;
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
                return Ok(result);
            }
            return BadRequest(result);
        }
        // Kullanıcıyı aktif hale getiren endpoint
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var result = await _ilanService.ActivateIlan(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Kullanıcıyı devre dışı bırakan endpoint
        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateUser(int id)
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
