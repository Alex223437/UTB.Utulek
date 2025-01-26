using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Domain.Entities;
using UTB.Utulek.Infrastructure.Database;

namespace UTB.Utulek.Application.Services
{
    public class DonationService : IDonationService
    {
        private readonly UtulekDbContext _context;

        public DonationService(UtulekDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateDonationAsync(Donation donation)
        {
            if (donation == null)
            {
                throw new ArgumentNullException(nameof(donation));
            }

            donation.Id = Guid.NewGuid();
            donation.Date = DateTime.UtcNow;

            await _context.Donations.AddAsync(donation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Donation>> GetAllDonationsAsync()
        {
            return await _context.Donations
                .Include(d => d.User)
                .OrderByDescending(d => d.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Donation>> GetDonationsByUserAsync(Guid userId)
        {
            return await _context.Donations
                .Where(d => d.UserId == userId)
                .Include(d => d.User)
                .OrderByDescending(d => d.Date)
                .ToListAsync();
        }
    }
}