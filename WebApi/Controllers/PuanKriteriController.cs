using Business.Abstracts;
using Entities.Dtos.AlanKriteri;
using Entities.Dtos.PuanKriteri;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuanKriteriController : ControllerBase
    {
        private readonly IPuanKriteriService _puanKriteriService;

        public PuanKriteriController(IPuanKriteriService puanKriteriService)
        {
            _puanKriteriService = puanKriteriService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _puanKriteriService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _puanKriteriService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _puanKriteriService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdatePuanKriteriDto updatePuanKriteriDto)
        {
            var result = await _puanKriteriService.Update(updatePuanKriteriDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddPuanKriteriDto addPuanKriteriDto)
        {
            var result = await _puanKriteriService.Add(addPuanKriteriDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
