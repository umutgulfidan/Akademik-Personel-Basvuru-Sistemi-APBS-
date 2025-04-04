using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Email
{
    public class SendUserEmailDto
    {
        public int UserId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
