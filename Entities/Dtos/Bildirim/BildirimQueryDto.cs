using Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Bildirim
{
    public class BildirimQueryDto : BaseQueryDto
    {
        public int? Id { get; set; }
        public string? Baslik { get; set; }
        public int? KullaniciId { get; set; }
        public bool? Status { get; set; }
        public DateTime? MinTarih { get; set; }
        public DateTime? MaxTarih { get; set; }
    }
}
