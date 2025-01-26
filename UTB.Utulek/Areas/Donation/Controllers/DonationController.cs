using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UTB.Utulek.Application.Services;
using UTB.Utulek.Domain.Entities;

namespace UTB.Utulek.Areas.Donation.Controllers
{
    [Area("Donation")]
    [Authorize]
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;
        private readonly UserManager<User> _userManager;

        public DonationController(IDonationService donationService, UserManager<User> userManager)
        {
            _donationService = donationService ?? throw new ArgumentNullException(nameof(donationService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // GET: Donation/Donation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donation/Donation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Domain.Entities.Donation model)
        {
            Console.WriteLine($"Raw Amount: {Request.Form["Amount"]}");
            Console.WriteLine($"Raw Message: {Request.Form["Message"]}");
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.UserId = user.Id;

            await _donationService.CreateDonationAsync(model);

            return RedirectToAction("ThankYou");
        }

        // GET: Donation/Donation/ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }

        // GET: Donation/Donation/History
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> History()
        {
            var donations = await _donationService.GetAllDonationsAsync();
            return View(donations);
        }
    }
}