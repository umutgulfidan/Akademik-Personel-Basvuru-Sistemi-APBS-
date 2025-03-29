using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Ilan : BaseEntity,IEntity
    {
        [Key]
        public int Id { get; set; }

        public int OlusturanId { get; set; }

        [ForeignKey("OlusturanId")]
        public User Olusturan { get; set; }

        public int PozisyonId { get; set; }
        public Pozisyon Pozisyon { get; set; }


        public int BolumId { get; set; }
        public Bolum Bolum { get; set; }


        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Status { get; set; }



    }
}
