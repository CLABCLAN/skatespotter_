using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkateSpotter.Logic.Interfaces;
using SkateSpotter.Logic.Models;
using SkateSpotter.Presentation.Models;
using System.Security.Claims;

namespace SkateSpotter.Presentation.Controllers
{
    public class SpotController : Controller
    {
        private readonly ISpotService _spotService;
        private readonly IReviewService _reviewService;

        public SpotController(ISpotService spotService, IReviewService reviewService)
        {
            _spotService = spotService;
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            var spots = _spotService.GetAll();
            return View(spots);
        }

        public IActionResult Detail(int id)
        {
            var spot = _spotService.GetById(id);
            if (spot == null) return NotFound();

            spot.Reviews = _reviewService.GetBySpotId(id).ToList();
            return View(spot);
        }

        [Authorize]
        public IActionResult Create(decimal? lat, decimal? lng)
        {
            var vm = new SpotCreateViewModel
            {
                Latitude = lat ?? 0,
                Longitude = lng ?? 0
            };
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SpotCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var spot = new Spot
            {
                Name = vm.Name,
                Latitude = vm.Latitude,
                Longitude = vm.Longitude,
                CreatedUserId = userId
            };

            try
            {
                _spotService.Create(spot);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
    }
}