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
            var result = await _fileService.UploadFileAsync(file);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete()
        {
            var result = await _fileService.DeleteFileAsync("s3://apbs-file-storage/db08a4ce-7525-4eee-bc88-1e11f26349c4.pdf");
            return Ok(result);
        }
    }
}
