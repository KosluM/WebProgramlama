using System.ComponentModel.DataAnnotations;

namespace WebProgramlama.Models
{
    public class AppointmentStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StatusName { get; set; } // Pending, Approved, Cancelled
    }
}
