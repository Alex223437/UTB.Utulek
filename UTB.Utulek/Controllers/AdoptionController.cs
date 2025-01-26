using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Domain.Entities;
using UTB.Utulek.Infrastructure.Database;

namespace UTB.Utulek.Presentation.Controllers
{
    [Authorize] // Только авторизованные пользователи
    public class AdoptionController : Controller
    {
        private readonly UtulekDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdoptionController(UtulekDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Adoption Form
        [HttpGet]
        public async Task<IActionResult> Apply(Guid animalId)
        {
            var animal = await _context.Animals.FindAsync(animalId);
            if (animal == null)
            {
                return NotFound();
            }
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                Console.WriteLine("User is NULL in _userManager.");
                return RedirectToAction("Login", "Account");
            }
            
            ViewBag.UserEmail = user.Email;

            var model = new AdoptionApplication
            {
                AnimalId = animal.Id,
                Animal = animal
            };

            return View(model); // Переход к форме
        }

        // POST: Adoption Application Submission
        [HttpPost]
        public async Task<IActionResult> Apply(AdoptionApplication model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                // Создаём новую заявку
                model.Id = Guid.NewGuid();
                model.UserId = user.Id;
                model.Status = ApplicationStatus.New;
                model.ApplicationDate = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;

                _context.AdoptionApplications.Add(model);

                // Меняем статус животного на InProgress
                var animal = await _context.Animals.FindAsync(model.AnimalId);
                if (animal != null)
                {
                    animal.AdoptionStatus = AdoptionStatus.InProgress;
                    _context.Animals.Update(animal);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Animals"); // Возвращаем на список животных
            }

            return View(model);
        }
    }
}