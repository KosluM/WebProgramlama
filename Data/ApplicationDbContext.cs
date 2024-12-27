using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProgramlama.Models;

namespace WebProgramlama.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 🛠️ Kullanıcılar (IdentityUser kullanılıyor, bu sınıf IdentityDbContext'te tanımlıdır)
     
        // 🧑‍🤝‍🧑 Müşteri Tablosu
        public DbSet<Customer> Customers { get; set; }

        // 👨‍💼 Çalışan Tablosu
        public DbSet<Employee> Employees { get; set; }

        // 📅 Randevu Tablosu
        public DbSet<Appointment> Appointments { get; set; }

        // ✅ Randevu Durumları Tablosu
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

        // ⚙️ Model Oluşturma (Fluent API Ayarları)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Müşteri Tablosu Konfigürasyonu
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.FullName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
            });

            // Çalışan Tablosu Konfigürasyonu
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Specialties).HasMaxLength(200);
                entity.Property(e => e.AvailableHours).IsRequired();
            });

            // Randevu Tablosu Konfigürasyonu
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Service).IsRequired().HasMaxLength(100);
                entity.Property(a => a.DurationInMinutes).IsRequired();
                entity.Property(a => a.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(a => a.Status).IsRequired().HasMaxLength(50);

                // Randevu - Müşteri ilişkisi
                entity.HasOne(a => a.Customer)
                      .WithMany()
                      .HasForeignKey(a => a.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Randevu - Çalışan ilişkisi
                entity.HasOne(a => a.Employee)
                      .WithMany()
                      .HasForeignKey(a => a.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Randevu Durumları Tablosu Konfigürasyonu
            modelBuilder.Entity<AppointmentStatus>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.StatusName).IsRequired().HasMaxLength(50);
            });

            // Varsayılan Randevu Durumları
            modelBuilder.Entity<AppointmentStatus>().HasData(
                new AppointmentStatus { Id = 1, StatusName = "Pending" },
                new AppointmentStatus { Id = 2, StatusName = "Approved" },
                new AppointmentStatus { Id = 3, StatusName = "Cancelled" }
            );
        }
    }
}
