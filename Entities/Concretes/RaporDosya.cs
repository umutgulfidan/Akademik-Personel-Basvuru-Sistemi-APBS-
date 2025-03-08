using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concretes
{
    public class RaporDosya : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int BasvuruId { get; set; }
        public IlanBasvuru Basvuru { get; set; }

        public int JuriId { get; set; }
        public BasvuruJuri Juri { get; set; }

        public string DosyaYolu { get; set; }
        public DateTime YuklenmeTarihi { get; set; }

    }
}
