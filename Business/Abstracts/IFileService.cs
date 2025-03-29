using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<bool> DeleteFileAsync(string fileUrl);
        Task<byte[]> GetFileAsync(string fileUrl);
        Task<string> UpdateFileAsync(IFormFile file, string oldFileUrl);
    }
}
