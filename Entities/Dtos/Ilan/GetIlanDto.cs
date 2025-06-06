﻿using Core.Entities.Concrete;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Ilan
{

    public class GetIlanDto
    {
        public int Id { get; set; }
        public int PozisyonId { get; set; }
        public Entities.Concretes.Pozisyon Pozisyon { get; set; }
        public int BolumId { get; set; }
        public Entities.Concretes.Bolum Bolum { get; set; }
        public string Baslik { get; set; }
        public DateTime BitisTarihi { get; set; }
    }
}
