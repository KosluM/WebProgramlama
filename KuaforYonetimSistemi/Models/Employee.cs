using System.ComponentModel.DataAnnotations;

namespace KuaforYonetimSistemi.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Specialty { get; set; } // Uzmanlık Alanı

        public bool IsAvailable { get; set; } // Müsaitlik Durumu
    }
}
