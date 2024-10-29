using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UTB.Utulek.Domain.Entities;
using UTB.Utulek.Infrastructure.Database;

namespace UTB.Utulek.Web.Controllers
{
    public class AnimalController : Controller
    {
        private readonly UtulekDbContext _context;

        public AnimalController(UtulekDbContext context)
        {
            _context = context;
        }

        // GET: Animal
        public async Task<IActionResult> Index(string filter, string sort)
        {
            // Получаем всех животных
            var animals = _context.Animals.AsQueryable();

            // Применяем фильтр по возрасту
            if (!string.IsNullOrEmpty(filter))
            {
                animals = filter.ToLower() switch
                {
                    "mladý" => animals.Where(a => a.Age < 2), // Молодые животные
                    "dospělý" => animals.Where(a => a.Age >= 2), // Взрослые животные
                    _ => animals
                };
            }

            // Применяем сортировку
            animals = sort?.ToLower() switch
            {
                "name" => animals.OrderBy(a => a.Name),
                "age" => animals.OrderBy(a => a.Age),
                "breed" => animals.OrderBy(a => a.Breed),
                _ => animals
            };

            // Загружаем данные и возвращаем результат в представление
            return View(await animals.ToListAsync());
        }

        // GET: Animal/Details/5
        public async Task<IActionResult> Details(int id)
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