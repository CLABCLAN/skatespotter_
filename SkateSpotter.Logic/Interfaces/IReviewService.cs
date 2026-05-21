using SkateSpotter.Logic.Models;

namespace SkateSpotter.Logic.Interfaces
{
    public interface IReviewService
    {
        IEnumerable<Review> GetBySpotId(int spotId);
        void Create(Review review);
    }
}