using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class UserForRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NationalityId { get; set; }
        public string Password { get; set; }  // Şifreyi ekliyoruz
        public string ConfirmPassword { get; set; }  // Şifre doğrulama alanı
    }


}
