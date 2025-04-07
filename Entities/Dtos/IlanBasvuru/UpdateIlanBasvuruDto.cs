using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.IlanBasvuru
{
    public class UpdateIlanBasvuruDto
    {
        public int Id { get; set; }

        public int IlanId { get; set; }

        public int BasvuranId { get; set; }

        public int BasvuruDurumuId { get; set; }

        public DateTime BasvuruTarihi { get; set; }
        public string? Aciklama { get; set; }

    }
}
