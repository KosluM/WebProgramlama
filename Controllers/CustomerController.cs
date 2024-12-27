using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramlama.Data;
using WebProgramlama.Models;

namespace WebProgramlama.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer/CustomerDashboard
        public IActionResult CustomerDashboard()
        {
            var customerId = _context.Users
                .FirstOrDefault(u => u.UserName == User.Identity.Name)?
                .Id;

            if (customerId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var appointments = _context.Appointments
                .Include(a => a.Employee)
                .Where(a => a.CustomerId.ToString() == customerId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();

            return View(appointments);
        }

        // POST: Customer/CancelAppointment/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelAppointment(Guid id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null && appointment.Status == "Pending")
            {
                appointment.Status = "Cancelled";
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(CustomerDashboard));
        }

        // GET: Customer/EditProfile
        [HttpGet]
        public IActionResult EditProfile()
        {
            var customer = _context.Users
                .FirstOrDefault(u => u.UserName == User.Identity.Name);

            if (customer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new EditUserViewModel
            {
                Id = customer.Id,
                Email = customer.Email,
                UserName = customer.UserName
            };

            return View(model);
        }

        // POST: Customer/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProfile(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var customer = _context.Users
                .FirstOrDefault(u => u.UserName == User.Identity.Name);

            if (customer != null)
            {
                customer.Email = model.Email;
                customer.UserName = model.UserName;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(CustomerDashboard));
        }
    }
}
