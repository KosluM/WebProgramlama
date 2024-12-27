using System.ComponentModel.DataAnnotations;

namespace WebProgramlama.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
