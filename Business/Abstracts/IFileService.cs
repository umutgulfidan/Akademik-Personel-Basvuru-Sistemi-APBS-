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
        Task<string> UploadFileAsync(IFormFile file, string folderName = null);
        Task<bool> DeleteFileAsync(string key);
        Task<string> UpdateFileAsync(IFormFile file, string oldFileUrl, string folderName = null);
        Task<string> GetPreSignedUrlAsync(string objectKey, int expirationMinutes = 60);

    }
}
