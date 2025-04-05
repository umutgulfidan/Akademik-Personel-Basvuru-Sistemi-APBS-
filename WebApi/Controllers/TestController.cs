using Business.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IFileService _fileService;
        public TestController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await _fileService.UploadFileAsync(file , "Raporlar");
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string key)
        {
            var result = await _fileService.DeleteFileAsync(key);
            return Ok(result);
        }
        [HttpGet("geturl")]
        public async Task<IActionResult> GetUrl(string key)
        {
            var result = await _fileService.GetPreSignedUrlAsync(key, 10);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(IFormFile file,string key)
        {
            var result = await _fileService.UpdateFileAsync(file,key);
            return Ok(result);
        }
    }
}
