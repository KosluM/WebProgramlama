using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KuaforYonetimSistemi.Models;

namespace KuaforYonetimSistemi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Salon> Salons { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🏢 **Salon Yapılandırması**
            modelBuilder.Entity<Salon>(entity =>
            {
                entity.HasKey(s => s.ID);
                entity.Property(s => s.Name).IsRequired();
                entity.Property(s => s.Address).IsRequired(false);
                entity.Property(s => s.ContactInfo).IsRequired(false);
                entity.Property(s => s.OpeningHours).IsRequired();
                entity.Property(s => s.ClosingHours).IsRequired();
            });

            // 💼 **Service Yapılandırması**
            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(s => s.ServiceName).IsRequired();
                entity.Property(s => s.Duration).IsRequired();
                entity.Property(s => s.Price).IsRequired();

                entity.HasOne(s => s.Salon)
                      .WithMany()
                      .HasForeignKey(s => s.SalonID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 👤 **Employee Yapılandırması**
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            });

            // 📅 **Appointment Yapılandırması**
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasOne(a => a.Employee)
                       .WithMany()
                       .HasForeignKey(a => a.EmployeeId)
                       .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Service)
                      .WithMany()
                      .HasForeignKey(a => a.ServiceId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.User)
                      .WithMany()
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 🛡️ **User Yapılandırması**
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.FirstName).IsRequired();
                entity.Property(u => u.LastName).IsRequired();
                entity.Property(u => u.IsActive).HasDefaultValue(true);
            });
        }
    }
}
