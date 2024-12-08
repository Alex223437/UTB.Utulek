using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Infrastructure.Database;
using UTB.Utulek.Domain.Entities;

namespace UTB.Utulek.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionApplicationController : ControllerBase
    {
        private readonly UtulekDbContext _context;

        public AdoptionApplicationController(UtulekDbContext context)
        {
            _context = context;
        }

        // GET: api/AdoptionApplication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdoptionApplication>>> GetAdoptionApplications()
        {
            return await _context.AdoptionApplications.ToListAsync();
        }

        // GET: api/AdoptionApplication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdoptionApplication>> GetAdoptionApplication(Guid id)
        {
            var adoptionApplication = await _context.AdoptionApplications.FindAsync(id);

            if (adoptionApplication == null)
            {
                return NotFound();
            }

            return adoptionApplication;
        }

        // POST: api/AdoptionApplication
        [HttpPost]
        public async Task<ActionResult<AdoptionApplication>> CreateAdoptionApplication(AdoptionApplication adoptionApplication)
        {
            _context.AdoptionApplications.Add(adoptionApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdoptionApplication), new { id = adoptionApplication.Id }, adoptionApplication);
        }

        // PUT: api/AdoptionApplication/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdoptionApplication(Guid id, AdoptionApplication adoptionApplication)
        {
            if (id != adoptionApplication.Id)
            {
                return BadRequest();
            }

            _context.Entry(adoptionApplication).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/AdoptionApplication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdoptionApplication(Guid id)
        {
            var adoptionApplication = await _context.AdoptionApplications.FindAsync(id);
            if (adoptionApplication == null)
            {
                return NotFound();
            }

            _context.AdoptionApplications.Remove(adoptionApplication);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
