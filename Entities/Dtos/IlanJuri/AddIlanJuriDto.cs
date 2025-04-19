using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.IlanJuri
{
    public class AddIlanJuriDto
    {
        public int KullaniciId { get; set; }
        public int IlanId { get; set; }
    }
}
