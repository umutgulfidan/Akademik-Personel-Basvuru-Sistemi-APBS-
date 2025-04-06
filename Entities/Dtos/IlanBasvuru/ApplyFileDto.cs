using Microsoft.AspNetCore.Http;

namespace Entities.Dtos.IlanBasvuru
{
    public class ApplyFileDto
    {
        public IFormFile File { get; set; }
        public List<int> KriterIds { get; set; }
    }
}
