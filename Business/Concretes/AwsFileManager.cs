using Amazon.S3;
using Amazon.S3.Model;
using Business.Abstracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Concretes
{
    public class AwsFileManager : IFileService
    {
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _bucketName;
        private readonly string _r2Endpoint;

        private readonly AmazonS3Client _s3Client;

        public AwsFileManager()
        {
            DotNetEnv.Env.Load(); 

            _accessKey = Environment.GetEnvironmentVariable("CLOUDFLARE_ACCESS_KEY");
            _secretKey = Environment.GetEnvironmentVariable("CLOUDFLARE_SECRET_KEY");
            _bucketName = Environment.GetEnvironmentVariable("CLOUDFLARE_BUCKET_NAME");
            _r2Endpoint = Environment.GetEnvironmentVariable("CLOUDFLARE_ENDPOINT");

            var config = new AmazonS3Config
            {
                ServiceURL = _r2Endpoint,
                ForcePathStyle = true
            };

            _s3Client = new AmazonS3Client(_accessKey, _secretKey, config);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

            await _s3Client.PutObjectAsync(request);
            return $"{_r2Endpoint}/{_bucketName}/{fileName}";
        }

        public async Task<bool> DeleteFileAsync(string fileUrl)
        {
            var fileName = Path.GetFileName(fileUrl);
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            await _s3Client.DeleteObjectAsync(request);
            return true;
        }

        public async Task<byte[]> GetFileAsync(string fileUrl)
        {
            var fileName = Path.GetFileName(fileUrl);
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            using var response = await _s3Client.GetObjectAsync(request);
            using var memoryStream = new MemoryStream();
            await response.ResponseStream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<string> UpdateFileAsync(IFormFile file, string oldFileUrl)
        {
            await DeleteFileAsync(oldFileUrl);
            return await UploadFileAsync(file);
        }
    }
}
