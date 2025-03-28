using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Ilan
{
    public class UserIlanQueryDto
    {
        public int PageSize { get; set; } = 12;  // Varsayılan olarak 10 kullanıcı döndür
        public int PageNumber { get; set; } = 1; // Varsayılan olarak 1. sayfayı döndür
        public string SortBy { get; set; } = "Id"; // Varsayılan olarak ID'ye göre sıralama
        public bool IsDescending { get; set; } = false; // Varsayılan olarak artan sıralama

        // Yeni filtreleme alanları
        public int? PozisyonId { get; set; }
        public int? BolumId { get; set; }
        public int? Id { get; set; }
        public string? Baslik { get; set; }
        public IlanTuru? IlanTipi { get; set; }
    }

}
