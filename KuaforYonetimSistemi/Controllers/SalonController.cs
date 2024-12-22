using KuaforYonetimSistemi.Data;
using KuaforYonetimSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KuaforYonetimSistemi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SalonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 📌 Salon Listesi
        public IActionResult Index()
        {
            var salons = _context.Salons.ToList();
            return View(salons);
        }

        // 📌 Yeni Salon Ekle (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 📌 Yeni Salon Ekle (POST)
        [HttpPost]
        public IActionResult Create(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Salons.Add(salon);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salon);
        }

        // 📌 Salon Düzenle (GET)
        public IActionResult Edit(int id)
        {
            var salon = _context.Salons.Find(id);
            if (salon == null) return NotFound();
            return View(salon);
        }

        // 📌 Salon Düzenle (POST)
        [HttpPost]
        public IActionResult Edit(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Salons.Update(salon);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salon);
        }

        // 📌 Salon Sil
        public IActionResult Delete(int id)
        {
            var salon = _context.Salons.Find(id);
            if (salon != null)
            {
                _context.Salons.Remove(salon);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
