using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.AlanKriteri
{
    public class AddAlanKriteriDto
    {
        public int KriterId { get; set; }

        public int AlanId { get; set; }

        public int PozisyonId { get; set; }

        public int? MinAdet { get; set; }
    }

}
