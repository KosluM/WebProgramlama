using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using KuaforYonetimSistemi.Data;
using KuaforYonetimSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace KuaforYonetimSistemi.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Müşteri Randevu Görüntüleme
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var appointments = await _context.Appointments
                .Where(a => a.CustomerId == userId)
                .ToListAsync();

            return View(appointments);
        }

        // Randevu Oluşturma Sayfası (GET)
        [Authorize(Roles = "Customer")]
        public IActionResult Create()
        {
            return View();
        }

        // Randevu Oluşturma (POST)
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.CustomerId = _userManager.GetUserId(User);
                appointment.Status = "Pending";
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Randevunuz başarıyla oluşturuldu!";
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // Admin Randevu Yönetimi
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Manage()
        {
            var appointments = await _context.Appointments.Include(a => a.Customer).ToListAsync();
            return View(appointments);
        }

        // Admin Randevu Silme
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Manage");
        }
    }
}
