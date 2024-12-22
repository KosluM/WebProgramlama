using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuaforYonetimSistemi.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ServiceName { get; set; } = string.Empty;

        [Required]
        public int Duration { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("Salon")]
        public int SalonID { get; set; }
        public Salon Salon { get; set; }
    }
}
