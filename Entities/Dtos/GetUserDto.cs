using Core.Entities.Concrete;
using Entities.Dtos.UserOperationClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string NationalityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public List<GetUserOperationClaimDto>? OperationClaims { get; set; }

        public string? ImageUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
