using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UTB.Utulek.Domain.Entities.Interfaces;
using UTB.Utulek.Domain.Entities.Enums;

namespace UTB.Utulek.Domain.Entities
{
    public class Animal : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Species { get; set; }

        public string Breed { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public AdoptionStatus AdoptionStatus { get; set; }

        public int? AdopterId { get; set; } 
    }
}