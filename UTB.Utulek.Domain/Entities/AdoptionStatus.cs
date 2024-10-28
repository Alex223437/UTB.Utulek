using UTB.Utulek.Domain.Entities;

public class AdoptionStatus : Entity<int>
{
    public string StatusName { get; set; }
    public ICollection<AdoptionApplication> AdoptionApplications { get; set; } // Добавлено
}
