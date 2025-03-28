using Entities.Dtos.Bildirim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class NotificationTemplates
    {
        public static BildirimDto Info(string baslik, string aciklama)
        {
            return new BildirimDto
            {
                Baslik = baslik,
                Aciklama = aciklama,
                Icon = "fa-info-circle", 
                Renk = "#01A9CB"
            };
        }

        public static BildirimDto Success(string baslik, string aciklama)
        {
            return new BildirimDto
            {
                Baslik = baslik,
                Aciklama = aciklama,
                Icon = "fa-check",
                Renk = "#23ad64"
            };
        }

        public static BildirimDto Warning(string baslik, string aciklama)
        {
            return new BildirimDto
            {
                Baslik = baslik,
                Aciklama = aciklama,
                Icon = "fa-exclamation-triangle",
                Renk = "#FF941A"
            };
        }
        public static BildirimDto Error(string baslik, string aciklama)
        {
            return new BildirimDto
            {
                Baslik = baslik,
                Aciklama = aciklama,
                Icon = "fa-times",
                Renk = "#FF0000"
            };
        }
    }
}
