using SkateSpotter.Data.Models;

namespace SkateSpotter.Data.Interfaces
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAll();
        IEnumerable<Review> GetBySpotId(int spotId);
        Review? GetById(int id);
        void Create(Review review);
    }
}
