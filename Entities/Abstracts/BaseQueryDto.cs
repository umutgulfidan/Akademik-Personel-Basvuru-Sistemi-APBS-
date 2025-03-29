using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstracts
{
    public abstract class BaseQueryDto
    {
        public int PageSize { get; set; } = 12;  // Varsayılan olarak 10 kullanıcı döndür
        public int PageNumber { get; set; } = 1; // Varsayılan olarak 1. sayfayı döndür
        public string SortBy { get; set; } = "Id"; // Varsayılan olarak ID'ye göre sıralama
        public bool IsDescending { get; set; } = false; // Varsayılan olarak artan sıralama
    }
}
