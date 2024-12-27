using System.ComponentModel.DataAnnotations;

namespace WebProgramlama.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Specialty { get; set; } // Uzmanlık Alanı

        [Required]
        public string AvailableHours { get; set; } // Müsaitlik Saatleri

        [Required]
        public string Specialties { get; set; } // Uzmanlık Detayları (Genişletilmiş)
    }
}
