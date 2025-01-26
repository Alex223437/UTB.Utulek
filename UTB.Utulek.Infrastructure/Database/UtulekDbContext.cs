using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Domain.Entities;

namespace UTB.Utulek.Infrastructure.Database
{
    public class UtulekDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public UtulekDbContext(DbContextOptions<UtulekDbContext> options)
            : base(options)
        {
        }

        // DbSet для сущностей
        public DbSet<Animal> Animals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; }
        
        public DbSet<VolunteerSchedule> VolunteerSchedules { get; set; }
        
        public DbSet<Donation> Donations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация сущностей
            ConfigureUser(modelBuilder);
            ConfigureAnimal(modelBuilder);
            ConfigureAdoptionApplication(modelBuilder);
            ConfigureVolunteerSchedule(modelBuilder);
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Настройка enum UserRole как строки
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();
        }

        private void ConfigureAnimal(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasKey(a => a.Id);

            // Настройка enum AdoptionStatus как строки
            modelBuilder.Entity<Animal>()
                .Property(a => a.AdoptionStatus)
                .HasConversion<string>();
        }

        private void ConfigureAdoptionApplication(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdoptionApplication>()
                .HasKey(aa => aa.Id);

            // Связь с User
            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(aa => aa.User)
                .WithMany(u => u.AdoptionApplications)
                .HasForeignKey(aa => aa.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Animal
            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(aa => aa.Animal)
                .WithMany(a => a.AdoptionApplications)
                .HasForeignKey(aa => aa.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка enum ApplicationStatus как строки
            modelBuilder.Entity<AdoptionApplication>()
                .Property(aa => aa.Status)
                .HasConversion<string>();
        }
        
        private void ConfigureVolunteerSchedule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VolunteerSchedule>()
                .HasKey(vs => vs.Id);

            // Связь с User (Volunteer)
            modelBuilder.Entity<VolunteerSchedule>()
                .HasOne(vs => vs.Volunteer)
                .WithMany()
                .HasForeignKey(vs => vs.VolunteerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
