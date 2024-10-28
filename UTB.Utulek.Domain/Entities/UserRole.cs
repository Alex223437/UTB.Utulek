using System.Collections.Generic;
using UTB.Utulek.Domain.Entities.Interfaces;

namespace UTB.Utulek.Domain.Entities
{
    public class UserRole : Entity<int>
    {
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
