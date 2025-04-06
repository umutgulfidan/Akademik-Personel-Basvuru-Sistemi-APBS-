using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concretes
{
    public class IlanBasvuruDosya : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int BasvuruId { get; set; }
        public IlanBasvuru Basvuru { get; set; }

        public int KriterId { get; set; }
        public Kriter Kriter { get; set; }
        public string DosyaYolu { get; set; }
        public DateTime YuklenmeTarihi { get; set; }

    }
}
