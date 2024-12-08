using System;

namespace UTB.Utulek.Domain.Entities
{
    public class VolunteerSchedule
    {
        public Guid Id { get; set; }
        public Guid VolunteerId { get; set; }
        public DateTime Date { get; set; }
        public string TaskDescription { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Навигационное свойство
        public User? Volunteer { get; set; }
    }
}
