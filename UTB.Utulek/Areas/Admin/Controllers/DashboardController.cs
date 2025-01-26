using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Domain.Entities;
using UTB.Utulek.Infrastructure.Database;

namespace UTB.Utulek.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly UtulekDbContext _context;

        public DashboardController(UserManager<User> userManager, UtulekDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // Главная страница панели администратора
        public IActionResult Index()
        {
            return View();
        }

        // Список пользователей
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // Редактирование пользователя (GET)
        [HttpGet]
        public async Task<IActionResult> EditUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Редактирование пользователя (POST)
        [HttpPost]
        public async Task<IActionResult> EditUser(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            // Обновляем поля пользователя
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            user.City = model.City;
            user.ZIP = model.ZIP;
            user.Role = model.Role; // Обновляем роль пользователя
            user.UpdatedAt = DateTime.UtcNow; // Обновляем дату изменения

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Users");
            }

            // Добавляем ошибки обновления в ModelState
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(user);
        }

        // Удаление пользователя
        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Users");
            }

            return BadRequest("Unable to delete user.");
        }

        // Список животных
        public async Task<IActionResult> Animals()
        {
            var animals = await _context.Animals.ToListAsync();
            return View(animals);
        }

        // Редактирование животного (GET)
        [HttpGet]
        public async Task<IActionResult> EditAnimal(Guid id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // Редактирование животного (POST)
        [HttpPost]
        public async Task<IActionResult> EditAnimal(Animal model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var animal = await _context.Animals.FindAsync(model.Id);
            if (animal == null)
            {
                return NotFound();
            }

            // Обновляем поля животного
            animal.Name = model.Name;
            animal.Age = model.Age;
            animal.Description = model.Description;
            animal.Species = model.Species;
            animal.Breed = model.Breed;
            animal.HealthStatus = model.HealthStatus;
            animal.AdoptionStatus = model.AdoptionStatus;

            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();

            return RedirectToAction("Animals");
        }

        // Удаление животного
        [HttpPost]
        public async Task<IActionResult> DeleteAnimal(Guid id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return RedirectToAction("Animals");
        }
        
        public async Task<IActionResult> AdoptionApplications()
        {
            var applications = await _context.AdoptionApplications
                .Include(a => a.Animal)
                .Include(a => a.User)
                .ToListAsync();

            return View(applications);
        }

        // GET: Edit Adoption Application
        [HttpGet]
        public async Task<IActionResult> EditApplication(Guid id)
        {
            var application = await _context.AdoptionApplications
                .Include(a => a.Animal)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Edit Adoption Application
        [HttpPost]
        public async Task<IActionResult> EditApplication(AdoptionApplication model)
        {
            var application = await _context.AdoptionApplications
                .Include(a => a.Animal)
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (application == null)
            {
                return NotFound();
            }

            // Обновляем поля заявки
            application.HasOtherAnimals = model.HasOtherAnimals;
            application.HasYardSpace = model.HasYardSpace;
            application.UserComment = model.UserComment;
            application.Status = model.Status;
            application.UpdatedAt = DateTime.UtcNow;

            // Обновляем статус животного
            if (application.Animal != null)
            {
                application.Animal.AdoptionStatus = model.Status switch
                {
                    ApplicationStatus.Approved => AdoptionStatus.Adopted,
                    ApplicationStatus.Rejected => AdoptionStatus.Available,
                    _ => AdoptionStatus.InProgress
                };
            }

            _context.AdoptionApplications.Update(application);
            if (application.Animal != null)
            {
                _context.Animals.Update(application.Animal);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("AdoptionApplications");
        }

        // POST: Delete Adoption Application
        [HttpPost]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {
            var application = await _context.AdoptionApplications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.AdoptionApplications.Remove(application);
            await _context.SaveChangesAsync();

            return RedirectToAction("AdoptionApplications");
        }
    }
}