using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Alan
{
    public class UpdateAlanDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string? Aciklama { get; set; }
    }
}
