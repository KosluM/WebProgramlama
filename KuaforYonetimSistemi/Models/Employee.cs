using System.ComponentModel.DataAnnotations;

namespace KuaforYonetimSistemi.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Çalışan adı boş bırakılamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Uzmanlık alanı boş bırakılamaz.")]
        public string Specialty { get; set; }

        [Required(ErrorMessage = "Çalışma başlangıç saati boş bırakılamaz.")]
        public TimeSpan AvailabilityStart { get; set; }

        [Required(ErrorMessage = "Çalışma bitiş saati boş bırakılamaz.")]
        public TimeSpan AvailabilityEnd { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true; // Varsayılan olarak uygun

        public string Notes { get; set; }

        [Required]
        public string Salon { get; set; } // Hangi salona bağlı olduğu

        // New property added for assignment of employees to appointments
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
