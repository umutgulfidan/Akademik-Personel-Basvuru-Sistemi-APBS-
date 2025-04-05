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
        public async Task<IActionResult> Delete()
        {
            var result = await _fileService.DeleteFileAsync("https://apbs-file-storage.s3.us-east-1.amazonaws.com/Raporlar/glioma5.jpg");
            return Ok(result);
        }
    }
}
