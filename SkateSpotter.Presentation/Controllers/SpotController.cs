using Microsoft.AspNetCore.Mvc;
using SkateSpotter.Data.Interfaces;

namespace SkateSpotter.Presentation.Controllers
{
    public class SpotController : Controller
    {
        private readonly ISpotRepository _repo;

        public SpotController(ISpotRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var spot = _repo.GetAll();
            return View(spot);
        }
    }
}
