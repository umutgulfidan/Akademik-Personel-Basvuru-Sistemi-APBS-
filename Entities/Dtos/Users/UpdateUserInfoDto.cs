using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Users
{
    public class UpdateUserInfoDto
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
    }
    
}
