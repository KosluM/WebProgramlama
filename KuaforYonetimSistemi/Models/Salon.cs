using System.ComponentModel.DataAnnotations;

namespace KuaforYonetimSistemi.Models
{
    public class Salon
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactInfo { get; set; }

        [Required]
        public string OpeningHours { get; set; }

        [Required]
        public string ClosingHours { get; set; }
    }
}
