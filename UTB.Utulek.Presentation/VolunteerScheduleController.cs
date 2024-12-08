using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Infrastructure.Database;
using UTB.Utulek.Domain.Entities;

namespace UTB.Utulek.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerScheduleController : ControllerBase
    {
        private readonly UtulekDbContext _context;

        public VolunteerScheduleController(UtulekDbContext context)
        {
            _context = context;
        }

        // GET: api/VolunteerSchedule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolunteerSchedule>>> GetVolunteerSchedules()
        {
            return await _context.VolunteerSchedules.ToListAsync();
        }

        // GET: api/VolunteerSchedule/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VolunteerSchedule>> GetVolunteerSchedule(Guid id)
        {
            var volunteerSchedule = await _context.VolunteerSchedules.FindAsync(id);

            if (volunteerSchedule == null)
            {
                return NotFound();
            }

            return volunteerSchedule;
        }

        // POST: api/VolunteerSchedule
        [HttpPost]
        public async Task<ActionResult<VolunteerSchedule>> CreateVolunteerSchedule(VolunteerSchedule volunteerSchedule)
        {
            _context.VolunteerSchedules.Add(volunteerSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVolunteerSchedule), new { id = volunteerSchedule.Id }, volunteerSchedule);
        }

        // PUT: api/VolunteerSchedule/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVolunteerSchedule(Guid id, VolunteerSchedule volunteerSchedule)
        {
            if (id != volunteerSchedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(volunteerSchedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/VolunteerSchedule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolunteerSchedule(Guid id)
        {
            var volunteerSchedule = await _context.VolunteerSchedules.FindAsync(id);
            if (volunteerSchedule == null)
            {
                return NotFound();
            }

            _context.VolunteerSchedules.Remove(volunteerSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
