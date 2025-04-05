using Business.Abstracts;
using Entities.Dtos.Alan;
using Entities.Dtos.BasvuruDurumu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasvuruDurumuController : ControllerBase
    {
        private readonly IBasvuruDurumuService _basvuruDurumuService;

        public BasvuruDurumuController(IBasvuruDurumuService basvuruDurumuService)
        {
            _basvuruDurumuService = basvuruDurumuService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _basvuruDurumuService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _basvuruDurumuService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _basvuruDurumuService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateBasvuruDurumuDto updateBasvuruDurumuDto)
        {
            var result = await _basvuruDurumuService.Update(updateBasvuruDurumuDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddBasvuruDurumuDto addBasvuruDurumuDto)
        {
            var result = await _basvuruDurumuService.Add(addBasvuruDurumuDto);
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
