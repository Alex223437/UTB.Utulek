using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Domain.Entities;

namespace UTB.Utulek.Infrastructure.Database
{
    public class UtulekDbContext : DbContext
    {
        public UtulekDbContext(DbContextOptions<UtulekDbContext> options)
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

            // Указываем первичные ключи
            modelBuilder.Entity<Animal>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<AdoptionApplication>()
                .HasKey(aa => aa.Id);

            modelBuilder.Entity<AdoptionStatus>()
                .HasKey(ads => ads.Id);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.Id);

            modelBuilder.Entity<ApplicationStatus>()
                .HasKey(aps => aps.Id);

            // Настройка связей

            // 1. Связь между User и AdoptionApplication (One-to-Many)
            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(aa => aa.User) // Свойство в AdoptionApplication, ссылающееся на User
                .WithMany(u => u.AdoptionApplications) // Коллекция заявок у User
                .HasForeignKey(aa => aa.UserId) // Внешний ключ в AdoptionApplication
                .OnDelete(DeleteBehavior.Cascade); // Опционально, задает каскадное удаление

            // 2. Связь между Animal и AdoptionApplication (One-to-Many)
            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(aa => aa.Animal) // Свойство в AdoptionApplication, ссылающееся на Animal
                .WithMany(a => a.AdoptionApplications) // Коллекция заявок у Animal
                .HasForeignKey(aa => aa.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. Связь между User и UserRole (Many-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole) // Свойство в User, ссылающееся на UserRole
                .WithMany(ur => ur.Users) // Коллекция пользователей у роли
                .HasForeignKey(u => u.UserRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // 4. Связь между AdoptionStatus и AdoptionApplication (One-to-Many)
            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(aa => aa.AdoptionStatus) // Свойство в AdoptionApplication, ссылающееся на AdoptionStatus
                .WithMany(ads => ads.AdoptionApplications) // Коллекция заявок у статуса
                .HasForeignKey(aa => aa.AdoptionStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // 5. Связь между ApplicationStatus и AdoptionApplication (One-to-Many)
            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(aa => aa.ApplicationStatus) // Свойство в AdoptionApplication, ссылающееся на ApplicationStatus
                .WithMany(aps => aps.AdoptionApplications) // Коллекция заявок у статуса
                .HasForeignKey(aa => aa.ApplicationStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Здесь можно добавить любые дополнительные конфигурации
        }
    }
}
