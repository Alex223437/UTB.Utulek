using UTB.Utulek.Domain.Entities.Interfaces;
using UTB.Utulek.Domain.Entities;

public class AdoptionApplication : Entity<int>, IEntity<int>
{
    public int AnimalId { get; set; }
    public int UserId { get; set; }
    public DateTime ApplicationDate { get; set; }
    public int AdoptionStatusId { get; set; } 
    public int ApplicationStatusId { get; set; } 

    public User User { get; set; }
    public Animal Animal { get; set; }
    public AdoptionStatus AdoptionStatus { get; set; } 
    public ApplicationStatus ApplicationStatus { get; set; } 
}
