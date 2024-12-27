using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramlama.Models
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Service { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Status { get; set; } // Pending, Approved, Completed, Cancelled
    }
}
