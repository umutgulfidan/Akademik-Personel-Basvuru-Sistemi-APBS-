using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserForLoginDto
    {
        public string NationalityId { get; set; }  // Kullanıcının kimlik numarası
        public string Password { get; set; }       // Kullanıcının şifresi
    }

}
