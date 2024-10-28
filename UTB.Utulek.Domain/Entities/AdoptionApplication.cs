using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UTB.Utulek.Domain.Entities.Enums;

namespace UTB.Utulek.Domain.Entities
{
    public class AdoptionApplication : Entity<int>
    {
        public int AnimalId { get; set; } 
        public int UserId { get; set; } 
        public DateTime ApplicationDate { get; set; }

        public ApplicationStatus Status { get; set; }
    }
}