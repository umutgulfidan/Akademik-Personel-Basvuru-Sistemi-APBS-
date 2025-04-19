using Business.Abstracts;
using Entities.Dtos.Alan;
using Entities.Dtos.IlanJuri;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlanJuriController : ControllerBase
    {
        private readonly IIlanJuriService _ilanJuriService;

        public IlanJuriController(IIlanJuriService ilanJuriService)
        {
            _ilanJuriService = ilanJuriService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ilanJuriService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ilanJuriService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyilanid")]
        public async Task<IActionResult> GetByIlanId(int ilanId)
        {
            var result = await _ilanJuriService.GetByIlanId(ilanId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ilanJuriService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add(AddIlanJuriDto dto)
        {
            var result = await _ilanJuriService.Add(dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
