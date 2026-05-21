using SkateSpotter.Logic.Models;

namespace SkateSpotter.Logic.Interfaces
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAll();
        IEnumerable<Review> GetBySpotId(int spotId);
        Review? GetById(int id);
        void Create(Review review);
    }
}