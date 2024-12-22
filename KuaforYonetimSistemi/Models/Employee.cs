using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuaforYonetimSistemi.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Specialty { get; set; } = string.Empty;

        public string AvailabilityStart { get; set; } = string.Empty;
        public string AvailabilityEnd { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;

        [ForeignKey("Salon")]
        public int SalonID { get; set; }
        public Salon Salon { get; set; }
    }
}
