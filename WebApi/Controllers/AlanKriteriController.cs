using Business.Abstracts;
using Entities.Dtos.AlanKriteri;
using Entities.Dtos.Kriter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlanKriteriController : ControllerBase
    {
        private readonly IAlanKriteriService _alanKriteriService;

        public AlanKriteriController(IAlanKriteriService alanKriteriService)
        {
            _alanKriteriService = alanKriteriService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _alanKriteriService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _alanKriteriService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _alanKriteriService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateAlanKriteriDto updateAlanKriteriDto)
        {
            var result = await _alanKriteriService.Update(updateAlanKriteriDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddAlanKriteriDto addAlanKriteriDto)
        {
            var result = await _alanKriteriService.Add(addAlanKriteriDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
