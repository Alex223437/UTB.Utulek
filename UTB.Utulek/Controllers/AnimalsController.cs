using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Infrastructure.Database;
using UTB.Utulek.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace UTB.Utulek.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly UtulekDbContext _context;

        public AnimalsController(UtulekDbContext context)
        {
            _context = context;
        }
        
        // GET: Animals
        public async Task<IActionResult> Index(string sortOrder, int page = 1, int pageSize = 8)
        {
            // Сортировка
            var animalsQuery = _context.Animals.AsQueryable();

            animalsQuery = sortOrder switch
            {
                "name" => animalsQuery.OrderBy(a => a.Name),
                "age" => animalsQuery.OrderBy(a => a.Age),
                "date" => animalsQuery.OrderBy(a => a.ArrivalDate),
                "date_desc" => animalsQuery.OrderByDescending(a => a.ArrivalDate),
                _ => animalsQuery
            };

            ViewData["CurrentSort"] = sortOrder;

            // Общее количество животных
            var totalAnimals = await animalsQuery.CountAsync();

            // Вычисляем данные для текущей страницы
            var animals = await animalsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Передаем данные в представление
            ViewBag.TotalPages = (int)Math.Ceiling(totalAnimals / (double)pageSize);
            ViewBag.CurrentPage = page;

            return View(animals);
        }

        // GET: Animals/Add
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        // POST: Animals/Add
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(Animal animal, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Создаём путь для сохранения файла
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Сохраняем файл на сервер
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Сохраняем путь в базе данных
                animal.ImageUrl = "/uploads/" + uniqueFileName;
            }

            // Устанавливаем даты
            animal.CreatedAt = DateTime.UtcNow;
            animal.UpdatedAt = DateTime.UtcNow;
            animal.ArrivalDate = DateTime.UtcNow;

            // Сохраняем данные в базе
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Добавление тестового животного
        public async Task<IActionResult> AddTestAnimal()
        {
            var animal = new Animal
            {
                Id = Guid.NewGuid(),
                Name = "Dodik",
                Age = 1,
                ImageUrl = "asdasd" // Можно добавить путь к картинке
            };

            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Details(Guid id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }
        
    }
}