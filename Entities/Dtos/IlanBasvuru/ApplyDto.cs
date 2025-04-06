using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.IlanBasvuru
{
    public class ApplyDto
    {
        public int IlanId { get; set; }
        public DateTime BasvuruTarihi { get; set; }
        public string? Aciklama { get; set; }

        public List<ApplyFileDto> Files { get; set; }

    }
}
