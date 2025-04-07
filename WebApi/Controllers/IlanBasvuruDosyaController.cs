using Business.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlanBasvuruDosyaController : ControllerBase
    {
        private readonly IIlanBasvuruDosyaService _ilanBasvuruDosyaService;
        private readonly IFileService _fileService;

        public IlanBasvuruDosyaController(IIlanBasvuruDosyaService ilanBasvuruDosyaService, IFileService fileService)
        {
            _ilanBasvuruDosyaService = ilanBasvuruDosyaService;
            _fileService = fileService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ilanBasvuruDosyaService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result); // Success: Return 200 OK with result
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result); // Error: Return 500 status code
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ilanBasvuruDosyaService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetByBasvuruId")]
        public async Task<IActionResult> GetByIlanId(int basvuruId)
        {
            var result = await _ilanBasvuruDosyaService.GetByBasvuruId(basvuruId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
