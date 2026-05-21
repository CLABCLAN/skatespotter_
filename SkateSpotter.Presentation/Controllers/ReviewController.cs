using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkateSpotter.Logic.Interfaces;
using SkateSpotter.Logic.Models;
using SkateSpotter.Presentation.Models;
using System.Security.Claims;

namespace SkateSpotter.Presentation.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize]
        public IActionResult Create(int spotId)
        {
            return View(new ReviewCreateViewModel { SpotId = spotId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var review = new Review
            {
                SpotId = vm.SpotId,
                Rating = vm.Rating,
                Comment = vm.Comment,
                UserId = userId
            };

            try
            {
                _reviewService.Create(review);
                return RedirectToAction("Detail", "Spot", new { id = vm.SpotId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
    }
}