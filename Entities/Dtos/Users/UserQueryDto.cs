using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Users
{
    public class UserQueryDto
    {

        public int PageSize { get; set; } = 12;  // Varsayılan olarak 10 kullanıcı döndür
        public int PageNumber { get; set; } = 1; // Varsayılan olarak 1. sayfayı döndür
        public string SortBy { get; set; } = "Id"; // Varsayılan olarak ID'ye göre sıralama
        public bool IsDescending { get; set; } = false; // Varsayılan olarak artan sıralama

        // Yeni filtreleme alanları
        public string? SearchTerm { get; set; }
        public int? Id  { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalityId { get; set; }
        public string? Email { get; set; }
        public DateTime? MinDateOfBirth { get; set; }
        public DateTime? MaxDateOfBirth { get; set; }
        public bool? Status { get; set; } // Kullanıcı Durumu (aktif, pasif vb.)
    }
}
