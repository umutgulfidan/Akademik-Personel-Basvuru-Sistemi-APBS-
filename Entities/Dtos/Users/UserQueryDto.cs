using Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Users
{
    public class UserQueryDto : BaseQueryDto
    {

        // Yeni filtreleme alanları
        public string? SearchTerm { get; set; }
        public int? Id  { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalityId { get; set; }
        public string? Email { get; set; }
        public DateTime? MinDateOfBirth { get; set; }
        public DateTime? MaxDateOfBirth { get; set; }
        public bool? Status { get; set; } // Kullanıcı Durumu (aktif, pasif vb.)
    }
}
