using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UTB.Utulek.Domain.Entities;

namespace UTB.Utulek.Application.Services
{
    public interface IDonationService
    {
        Task CreateDonationAsync(Donation donation);
        Task<IEnumerable<Donation>> GetAllDonationsAsync();
        Task<IEnumerable<Donation>> GetDonationsByUserAsync(Guid userId);
    }
}