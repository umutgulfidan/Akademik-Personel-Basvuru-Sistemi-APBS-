using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concretes
{
    public class Bolum : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int AlanId { get; set; }
        public Alan Alan { get; set; }

        public string Ad { get; set; }
        public string? Aciklama { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public string? Adres { get; set; }
    }
}
