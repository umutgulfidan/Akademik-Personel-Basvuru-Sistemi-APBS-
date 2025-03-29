using Entities.Abstracts;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Ilan
{
    public class AdminIlanQueryDto : BaseQueryDto
    {

        // Yeni filtreleme alanları
        public int? PozisyonId { get; set; }
        public int? BolumId { get; set; }
        public int? Id { get; set; }
        public string? Baslik { get; set; }
        public bool? Status { get; set; }
        public IlanTuru? IlanTipi { get; set; }

        public override string ToString()
        {
            return $"{PageSize}-{PageNumber}-{SortBy}-{IsDescending}-" +
                   $"{Id?.ToString() ?? "null"}-" +
                   $"{PozisyonId?.ToString() ?? "null"}-" +
                   $"{BolumId?.ToString() ?? "null"}-" +
                   $"{Baslik ?? "null"}-" +
                   $"{IlanTipi?.ToString() ?? "null"}"+
                   $"{Status?.ToString() ?? "null"}-";
        }
    }
}
