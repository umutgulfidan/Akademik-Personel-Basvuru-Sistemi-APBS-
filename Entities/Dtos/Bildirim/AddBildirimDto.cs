using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Bildirim
{
    public class AddBildirimDto
    {
        public int KullaniciId { get; set; }  // Kullanıcı ID (Hangi kullanıcıya gidecek)
        public string Baslik { get; set; }
        public string Aciklama { get; set; }  // Bildirim mesajı
        public string Icon { get; set; }  // Bildirim ikonu (Opsiyonel)
        public string Renk { get; set; }  // Bildirim rengi (Opsiyonel)
    }
}
