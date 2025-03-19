using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Ilan
{
    public class UpdateIlanDto
    {
        public int Id { get; set; }
        public int PozisyonId { get; set; }
        public int BolumId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Status { get; set; }
    }
}
