using System;

namespace UTB.Utulek.Domain.Entities
{
    public class AdoptionApplication
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AnimalId { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.New;
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Новые поля
        public bool HasOtherAnimals { get; set; } // Есть ли другие животные
        public bool HasYardSpace { get; set; } // Есть ли двор/ограждённое пространство
        public string? UserComment { get; set; } // Комментарий пользователя

        // Навигационные свойства
        public User? User { get; set; }
        public Animal? Animal { get; set; }
    }

    public enum ApplicationStatus
    {
        New,
        Approved,
        Rejected
    }
}
