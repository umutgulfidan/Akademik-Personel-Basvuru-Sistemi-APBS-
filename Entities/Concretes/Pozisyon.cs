using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concretes
{
    public class Pozisyon : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string? Aciklama { get; set; }
    }
}
