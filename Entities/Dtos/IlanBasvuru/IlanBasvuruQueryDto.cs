using Core.Entities.Concrete;
using Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.IlanBasvuru
{
    public class IlanBasvuruQueryDto : BaseQueryDto
    {

        public int? Id { get; set; }
        public int? IlanId { get; set; }
        public int? BasvuranId { get; set; }
        public int? BasvuruDurumuId { get; set; }
        public DateTime? MinBasvuruTarihi { get; set; }
        public DateTime? MaxBasvuruTarihi { get; set; }
    }
}
