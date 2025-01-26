using System;
using System.Collections.Generic;

namespace UTB.Utulek.Domain.Entities
{
    public class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public HealthStatus HealthStatus { get; set; } = HealthStatus.Healthy;
        public bool IsAvailable { get; set; } = true;
        public DateTime ArrivalDate { get; set; } = DateTime.UtcNow;
        public AdoptionStatus AdoptionStatus { get; set; } = AdoptionStatus.Available;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // ������������� ��������
        public ICollection<AdoptionApplication> AdoptionApplications { get; set; } = new List<AdoptionApplication>();
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum AdoptionStatus
    {
        Available,
        InProgress,
        Adopted
    }

    public enum HealthStatus
    {
        Healthy,             // Здоровое
        Injured,             // Травмированное
        Sick,                // Больное
        Recovering,          // Выздоравливающее
        Disabled,            // С ограниченными возможностям
    }
}
