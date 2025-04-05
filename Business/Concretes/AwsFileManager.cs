using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Business.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class AwsFileManager : IFileService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private const string DEFAULT_FOLDER = "General";

        public AwsFileManager(IConfiguration configuration)
        {
            DotNetEnv.Env.Load(AppDomain.CurrentDomain.BaseDirectory + "\\.env");
            // .env dosyasından veya ortam değişkenlerinden kimlik bilgilerini al
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY");
            var region = Environment.GetEnvironmentVariable("AWS_REGION");
            _bucketName = Environment.GetEnvironmentVariable("AWS_BUCKET_NAME");

            // Değerler boş ise alternatif olarak IConfiguration'dan okuma dene
            if (string.IsNullOrEmpty(accessKey))
                accessKey = configuration["AWS:AccessKey"];
            if (string.IsNullOrEmpty(secretKey))
                secretKey = configuration["AWS:SecretKey"];
            if (string.IsNullOrEmpty(region))
                region = configuration["AWS:Region"];
            if (string.IsNullOrEmpty(_bucketName))
                _bucketName = configuration["AWS:BucketName"];

            // Gerekli bilgilerin var olduğunu kontrol et
            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey) ||
                string.IsNullOrEmpty(region) || string.IsNullOrEmpty(_bucketName))
            {
                throw new InvalidOperationException("AWS yapılandırma bilgileri eksik. Lütfen .env dosyasını veya appsettings.json dosyasını kontrol edin.");
            }

            // S3 istemcisini oluştur
            _s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
        }

        public async Task<string> GetPreSignedUrlAsync(string objectKey, int expirationMinutes = 60)
        {
            // GetPreSignedUrlRequest nesnesi oluşturuyoruz
            var request = new GetPreSignedUrlRequest
            {
                BucketName = this._bucketName,  // S3 bucket ismi
                Key = objectKey,  // Erişilmesi istenen dosyanın anahtarı (key)
                Expires = DateTime.Now.AddMinutes(expirationMinutes)  // URL'nin geçerlilik süresi
            };

            // Pre-signed URL oluşturuyoruz
            var preSignedUrl = await _s3Client.GetPreSignedURLAsync(request);

            return preSignedUrl;  // Pre-signed URL'yi döndürüyoruz
        }
        public async Task<string> UploadFileAsync(IFormFile file, string folderName = null)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Dosya boş olamaz", nameof(file));
            }

            try
            {
                // Klasör adını belirle, belirtilmemişse varsayılan klasörü kullan
                string folder = !string.IsNullOrWhiteSpace(folderName) ? folderName : DEFAULT_FOLDER;

                // Klasör adında başında veya sonunda '/' karakteri varsa temizle
                folder = folder.Trim('/');

                // Benzersiz bir dosya adı oluştur
                var fileExtension = Path.GetExtension(file.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var key = string.IsNullOrEmpty(folder) ? uniqueFileName : $"{folder}/{uniqueFileName}";

                using (var fileStream = file.OpenReadStream())
                {
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = fileStream,
                        BucketName = _bucketName,
                        Key = key,
                        ContentType = file.ContentType
                    };

                    var transferUtility = new TransferUtility(_s3Client);
                    await transferUtility.UploadAsync(uploadRequest);
                }

                // Yüklenen dosyanın URL'sini döndür
                return key;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                throw new Exception($"Dosya yükleme hatası: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteFileAsync(string s3Key)
        {
            try
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = s3Key
                };

                var response = await _s3Client.DeleteObjectAsync(deleteRequest);
                return response.HttpStatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya silme hatası: {ex.Message}", ex);
            }
        }

        public async Task<byte[]> GetFileAsync(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
            {
                throw new ArgumentException("Dosya URL'i boş olamaz", nameof(fileUrl));
            }

            try
            {
                // URL'den dosya yolunu çıkart (klasör dahil)
                var uri = new Uri(fileUrl);
                var key = uri.AbsolutePath.TrimStart('/');

                // Bucket adını URL'den çıkar (eğer varsa)
                if (key.StartsWith(_bucketName + "/"))
                {
                    key = key.Substring(_bucketName.Length + 1);
                }

                var request = new GetObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key
                };

                using (var response = await _s3Client.GetObjectAsync(request))
                using (var responseStream = response.ResponseStream)
                using (var memoryStream = new MemoryStream())
                {
                    await responseStream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                throw new Exception($"Dosya getirme hatası: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateFileAsync(IFormFile file, string oldKey, string folderName = null)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Dosya boş olamaz", nameof(file));

            if (string.IsNullOrEmpty(oldKey))
                throw new ArgumentException("Eski dosya key'i boş olamaz", nameof(oldKey));

            try
            {
                // Eski dosyayı sil
                await DeleteFileAsync(oldKey);

                // Klasör adı belirlenmediyse eski key'den çıkar
                if (string.IsNullOrWhiteSpace(folderName))
                {
                    var slashIndex = oldKey.LastIndexOf('/');
                    if (slashIndex > 0)
                        folderName = oldKey.Substring(0, slashIndex);
                    else
                        folderName = DEFAULT_FOLDER;
                }

                // Yeni dosyayı yükle
                return await UploadFileAsync(file, folderName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya güncelleme hatası: {ex.Message}", ex);
            }
        }

    }
}