using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concretes
{
    public class PuanKriteri : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int KriterId { get; set; }
        public Kriter Kriter { get; set; }

        public int AlanId { get; set; }
        public Alan Alan { get; set; }

        public int PozisyonId { get; set; }
        public Pozisyon Pozisyon { get; set; }

        public int? MinPuan { get; set; }
        public int? MaxPuan { get; set; }
    }
}
