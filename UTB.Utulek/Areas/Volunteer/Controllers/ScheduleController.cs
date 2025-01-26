using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Domain.Entities;
using UTB.Utulek.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace UTB.Utulek.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    [Authorize(Roles = "Volunteer,Admin")]
    public class ScheduleController : Controller
    {
        private readonly UtulekDbContext _context;
        private readonly UserManager<User> _userManager;

        public ScheduleController(UtulekDbContext context, UserManager<User> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // GET: Volunteer/Schedule
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.VolunteerSchedules
                .Include(v => v.Volunteer)
                .Where(t => !t.IsCompleted)
                .ToListAsync();
            return View(schedules);
        }

        // GET: Volunteer/Schedule/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var schedule = await _context.VolunteerSchedules
                .Include(v => v.Volunteer)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Volunteer/Schedule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Volunteer/Schedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VolunteerSchedule model)
        {
            // Получаем текущего авторизованного пользователя
            var user = await _userManager.GetUserAsync(User);
            if (user == null || (user.Role != UserRole.Volunteer && user.Role != UserRole.Admin))
            {
                ModelState.AddModelError("", "You must be a volunteer to create tasks.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                // Устанавливаем VolunteerId текущего пользователя
                model.VolunteerId = user.Id;
                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;

                _context.VolunteerSchedules.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Volunteer/Schedule/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var schedule = await _context.VolunteerSchedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {
                ViewBag.Volunteers = await _userManager.Users.Where(u => u.Role == UserRole.Volunteer).ToListAsync();
            }

            return View(schedule);
        }

        // POST: Volunteer/Schedule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VolunteerSchedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    schedule.UpdatedAt = DateTime.UtcNow;

                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the task. Please try again.");
                    Console.WriteLine(ex.InnerException?.Message);
                    return View(schedule);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(schedule);
        }

        // GET: Volunteer/Schedule/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var schedule = await _context.VolunteerSchedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Volunteer/Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var schedule = await _context.VolunteerSchedules.FindAsync(id);
            if (schedule != null)
            {
                _context.VolunteerSchedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index"); 
        }
        
        public async Task<IActionResult> History()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var tasks = await _context.VolunteerSchedules
                .Where(t =>  t.IsCompleted)
                .ToListAsync();

            return View(tasks);
        }
    }
}