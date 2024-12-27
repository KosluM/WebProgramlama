using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramlama.Data;
using WebProgramlama.Models;

namespace WebProgramlama.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointment/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Employees = _context.Employees.ToList();
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var isConflict = _context.Appointments.Any(a =>
                    a.EmployeeId == appointment.EmployeeId &&
                    a.AppointmentDate == appointment.AppointmentDate &&
                    a.Status == "Approved");

                if (isConflict)
                {
                    ModelState.AddModelError("", "Seçilen tarih ve saat dolu. Lütfen başka bir zaman seçin.");
                    ViewBag.Customers = _context.Customers.ToList();
                    ViewBag.Employees = _context.Employees.ToList();
                    return View(appointment);
                }

                appointment.Id = Guid.NewGuid();
                appointment.Status = "Pending";
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Employees = _context.Employees.ToList();
            return View(appointment);
        }

        // GET: Appointment/Index
        [HttpGet]
        public IActionResult Index()
        {
            var appointments = _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Employee)
                .ToList();

            return View(appointments);
        }

        // GET: Appointment/Edit/{id}
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Employees = _context.Employees.ToList();
            return View(appointment);
        }

        // POST: Appointment/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest("Güncellenen veri eşleşmiyor.");
            }

            if (ModelState.IsValid)
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Employees = _context.Employees.ToList();
            return View(appointment);
        }

        // POST: Appointment/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Appointment/Approve/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(Guid id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Status = "Approved";
            _context.Appointments.Update(appointment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
