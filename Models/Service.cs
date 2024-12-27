using System.ComponentModel.DataAnnotations;

namespace WebProgramlama.Models
{
    public class Service
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; } // Kadın, Erkek, Ortak
    }
}
