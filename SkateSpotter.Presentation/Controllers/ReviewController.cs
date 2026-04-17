using Microsoft.AspNetCore.Mvc;
using SkateSpotter.Data.Interfaces;

public class ReviewController : Controller
{
    private readonly IReviewRepository _repo;

    public ReviewController(IReviewRepository repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        var reviews = _repo.GetAll();
        return View(reviews);
    }

    public IActionResult BySpot(int id)
    {
        var reviews = _repo.GetBySpotId(id);
        return View(reviews);
    }

}
