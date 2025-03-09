using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Bolum
{
    public class AddBolumDto
    {
        public int AlanId { get; set; }
        public string Ad { get; set; }
        public string? Aciklama { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public string? Adres { get; set; }
    }
}
