using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuaforYonetimSistemi.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual IdentityUser Customer { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Service { get; set; }

        public string Notes { get; set; }

        [Required]
        public string Status { get; set; } // Pending, Confirmed, Canceled

        // New property added for employee assignment
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
