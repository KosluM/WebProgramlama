using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KuaforYonetimSistemi.Models;

namespace KuaforYonetimSistemi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Salon> Salons { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Specialty).IsRequired();
                entity.Property(e => e.AvailabilityStart).IsRequired();
                entity.Property(e => e.AvailabilityEnd).IsRequired();
                entity.Property(e => e.IsAvailable).IsRequired();
                entity.Property(e => e.Salon).IsRequired();
            });
        }
    }
}
