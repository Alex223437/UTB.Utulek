using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using UTB.Utulek.Domain.Entities;
using UTB.Utulek.Infrastructure.Database;

namespace UTB.Utulek.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly UtulekDbContext _context;

        public AdoptionController(UtulekDbContext context)
        {
            _context = context;
        }

        // GET: Adoption/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Name");
            return View();
        }

        // POST: Adoption/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdoptionApplication application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Animal"); // Redirect to the animal list page after submission
            }

            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Name", application.AnimalId);
            return View(application);
        }
    }
}