using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete
{

    public class UserOperationClaim : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int OperationClaimId { get; set; }
        public OperationClaim OperationClaim { get; set; }
    }
}