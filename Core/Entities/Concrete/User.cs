using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete
{
    public class User : BaseEntity,IEntity
    {
        [Key]
        public int Id { get; set; }
        public string NationalityId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string? ImageKey { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Status { get; set; }

    }
}