using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class BasvuruJuri : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int KullaniciId { get; set; }
        public User Kullanici { get; set; }

        public int BasvuruId { get; set; }
        public IlanBasvuru Basvuru { get; set; }

    }
}
