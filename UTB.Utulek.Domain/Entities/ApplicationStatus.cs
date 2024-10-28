using System.Collections.Generic;
using UTB.Utulek.Domain.Entities.Interfaces;

namespace UTB.Utulek.Domain.Entities
{
    public class ApplicationStatus : Entity<int>
    {
        public string StatusName { get; set; }

        // Добавьте это свойство
        public ICollection<AdoptionApplication> AdoptionApplications { get; set; }
    }
}
