using SkateSpotter.Logic.Interfaces;
using SkateSpotter.Logic.Models;

namespace SkateSpotter.Logic.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ISpotRepository _spotRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(ISpotRepository spotRepository, IReviewRepository reviewRepository)
        {
            _spotRepository = spotRepository;
            _reviewRepository = reviewRepository;
        }

        public IEnumerable<Review> GetBySpotId(int spotId) =>
            _reviewRepository.GetBySpotId(spotId);

        public void Create(Review review)
        {
            if (review.Rating < 1 || review.Rating > 5)
                throw new ArgumentException("Rating moet tussen 1 en 5 zijn.");

            var spot = _spotRepository.GetById(review.SpotId);

            if (spot == null)
                throw new ArgumentException("Spot bestaat niet.");

            if (spot.CreatedUserId == review.UserId)
                throw new ArgumentException("Je kunt geen review plaatsen op je eigen spot.");

            _reviewRepository.Create(review);
        }



        public IEnumerable<Review> GetAll()
        {
            throw new NotImplementedException();
        }

        public Review? GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}