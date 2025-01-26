using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UTB.Utulek.Models;
using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Infrastructure.Database;
using UTB.Utulek.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace UTB.Utulek.Controllers;

public class HomeController : Controller
{
    
    private readonly UtulekDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, UtulekDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public async Task<IActionResult> Index()
    {
        // Получаем случайное животное с фото
        var randomAnimal = await _context.Animals
            .OrderBy(a => EF.Functions.Random())          // Генерация случайного порядка
            .FirstOrDefaultAsync();

        return View(randomAnimal);
    }
}