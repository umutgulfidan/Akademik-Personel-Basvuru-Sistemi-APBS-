using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concretes;

namespace Entities.Dtos.AlanKriteri
{
    public class GetAlanKriteriDto
    {
        public int Id { get; set; }

        public int KriterId { get; set; }
        public Entities.Concretes.Kriter Kriter { get; set; }

        public int AlanId { get; set; }
        public Entities.Concretes.Alan Alan { get; set; }

        public int PozisyonId { get; set; }
        public Entities.Concretes.Pozisyon Pozisyon { get; set; }

        public int? MinAdet { get; set; }
    }
}
