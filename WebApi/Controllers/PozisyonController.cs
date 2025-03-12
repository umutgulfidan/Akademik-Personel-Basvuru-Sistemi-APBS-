using Business.Abstracts;
using Entities.Dtos.Bolum;
using Entities.Dtos.Pozisyon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PozisyonController : ControllerBase
    {
        private readonly IPozisyonService _pozisyonService;

        public PozisyonController(IPozisyonService pozisyonService)
        {
            this._pozisyonService = pozisyonService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _pozisyonService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _pozisyonService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _pozisyonService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdatePozisyonDto updatePozisyonDto)
        {
            var result = await _pozisyonService.Update(updatePozisyonDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddPozisyonDto addPozisyonDto)
        {
            var result = await _pozisyonService.Add(addPozisyonDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
