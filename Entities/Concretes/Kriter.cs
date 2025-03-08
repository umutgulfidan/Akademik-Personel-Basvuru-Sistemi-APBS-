using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Kriter : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string? Aciklama { get; set; }
    }
}
