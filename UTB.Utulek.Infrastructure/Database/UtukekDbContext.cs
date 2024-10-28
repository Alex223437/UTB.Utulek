using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Domain.Entities;

namespace UTB.Utulek.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet для каждой сущности
        public DbSet<Animal> Animals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; }
        public DbSet<AdoptionStatus> AdoptionStatuses { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация для сущностей (если необходимо)

            modelBuilder.Entity<Animal>()
                .HasKey(a => a.Id); // Убедитесь, что ваш класс Animal реализует IEntity<int>

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Убедитесь, что ваш класс User реализует IEntity<int>

            modelBuilder.Entity<AdoptionApplication>()
                .HasKey(aa => aa.Id); // Убедитесь, что ваш класс AdoptionApplication реализует IEntity<int>

            modelBuilder.Entity<AdoptionStatus>()
                .HasKey(ads => ads.Id); // Убедитесь, что ваш класс AdoptionStatus реализует IEntity<int>

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.Id); // Убедитесь, что ваш класс UserRole реализует IEntity<int>

            modelBuilder.Entity<ApplicationStatus>()
                .HasKey(aps => aps.Id); // Убедитесь, что ваш класс ApplicationStatus реализует IEntity<int>

            // Дополнительные конфигурации могут быть добавлены здесь
        }
    }
}