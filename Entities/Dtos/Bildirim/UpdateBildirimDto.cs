﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Bildirim
{
    public class UpdateBildirimDto
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Icon { get; set; }
        public string Renk { get; set; }
        public bool Status { get; set; }
    }
}
