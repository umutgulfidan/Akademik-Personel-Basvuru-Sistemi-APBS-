using Core.Entities;
using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concretes
{
    public class IlanBasvurusu : BaseEntity, IEntity
    {
        [Key]
        public int Id { get; set; }

        public int IlanId { get; set; }
        public Ilan Ilan { get; set; }

        public int BasvuranId { get; set; }
        public User Basvuran { get; set; }

        public int BasvuruDurumuId { get; set; }
        public BasvuruDurumu BasvuruDurumu { get; set; }

        public DateTime BasvuruTarihi { get; set; }
        public string? Aciklama { get; set; }
    }
}
