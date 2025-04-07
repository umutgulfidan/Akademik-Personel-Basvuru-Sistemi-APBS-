using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.IlanBasvuruDosya
{
    public class GetIlanBasvuruDosyaDto
    {
        public int Id { get; set; }
        public int BasvuruId { get; set; }
        public int KriterId { get; set; }
        public string DosyaYolu { get; set; }
        public string? DosyaUrl { get; set; }
        public DateTime YuklenmeTarihi { get; set; }
    }
}
