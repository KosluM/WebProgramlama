using System.ComponentModel.DataAnnotations;

namespace KuaforYonetimSistemi.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public int Duration { get; set; } // Dakika cinsinden

        [Required]
        public decimal Price { get; set; }
    }
}
