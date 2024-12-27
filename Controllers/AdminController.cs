using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramlama.Data;
using WebProgramlama.Models;

namespace WebProgramlama.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Admin/AdminDashboard
        public IActionResult AdminDashboard()
        {
            var totalAppointments = _context.Appointments.Count();
            var approvedAppointments = _context.Appointments.Count(a => a.Status == "Approved");
            var cancelledAppointments = _context.Appointments.Count(a => a.Status == "Cancelled");
            var totalIncome = _context.Appointments
                .Where(a => a.Status == "Approved")
                .Sum(a => a.Price);

            ViewBag.TotalAppointments = totalAppointments;
            ViewBag.ApprovedAppointments = approvedAppointments;
            ViewBag.CancelledAppointments = cancelledAppointments;
            ViewBag.TotalIncome = totalIncome;

            var users = _userManager.Users.ToList();
            return View(users);
        }

        // GET: Admin/EditUser/{id}
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(model);
        }

        // POST: Admin/EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = model.Email;
            user.UserName = model.UserName;
            var result = await _userManager.UpdateAsync(user);

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                var passwordRemoveResult = await _userManager.RemovePasswordAsync(user);
                if (passwordRemoveResult.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, model.NewPassword);
                }
            }

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AdminDashboard));
            }

            ModelState.AddModelError("", "Kullanıcı bilgileri güncellenirken bir hata oluştu.");
            return View(model);
        }

        // POST: Admin/DeleteUser/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Kullanıcı silinirken bir hata oluştu.");
                }
            }
            return RedirectToAction(nameof(AdminDashboard));
        }

        // GET: Admin/Appointments
        public IActionResult Appointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Employee)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();

            return View(appointments);
        }

        // POST: Admin/ApproveAppointment/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveAppointment(Guid id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = "Approved";
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Appointments));
        }

        // POST: Admin/CancelAppointment/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelAppointment(Guid id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = "Cancelled";
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Appointments));
        }

        // GET: Admin/Reports
        public IActionResult Reports()
        {
            var topEmployees = _context.Employees
                .OrderByDescending(e => _context.Appointments.Count(a => a.EmployeeId == e.Id && a.Status == "Approved"))
                .Take(5)
                .ToList();

            ViewBag.TopEmployees = topEmployees;

            var monthlyIncome = _context.Appointments
                .Where(a => a.Status == "Approved" && a.AppointmentDate.Month == DateTime.Now.Month)
                .Sum(a => a.Price);

            ViewBag.MonthlyIncome = monthlyIncome;

            return View();
        }
    }
}
