using System.ComponentModel.DataAnnotations;
using UTB.Utulek.Domain.Entities.Interfaces;
using UTB.Utulek.Domain.Entities.Enums;

namespace UTB.Utulek.Domain.Entities
{
    public class User : Entity<int>, IUser<int>
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserRole UserRole { get; set; }
    }
}