using Dapper;
using MySql.Data.MySqlClient;
using SkateSpotter.Logic.Interfaces;
using SkateSpotter.Logic.Models;
using SkateSpotter.Data.DTOs;

namespace SkateSpotter.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection CreateConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Review> GetAll()
        {
            using var conn = CreateConnection();
            var dtos = conn.Query<ReviewDto>("SELECT * FROM review");
            return dtos.Select(ToModel);
        }

        public IEnumerable<Review> GetBySpotId(int spotId)
        {
            using var conn = CreateConnection();
            const string sql = @"
        SELECT r.*, u.Username
        FROM review r
        LEFT JOIN user u ON r.UserId = u.UserId
        WHERE r.SpotId = @SpotId";

            return conn.Query<ReviewDto>(sql, new { SpotId = spotId }).Select(ToModel);
        }

        public Review? GetById(int id)
        {
            using var conn = CreateConnection();
            var dto = conn.QueryFirstOrDefault<ReviewDto>(
                "SELECT * FROM review WHERE ReviewId = @Id", new { Id = id });
            return dto == null ? null : ToModel(dto);
        }

        public void Create(Review review)
        {
            using var conn = CreateConnection();
            const string sql = @"
                INSERT INTO review (Rating, Comment, UserId, SpotId)
                VALUES (@Rating, @Comment, @UserId, @SpotId)";

            conn.Execute(sql, ToDto(review));
        }

        // Mapping
        private static Review ToModel(ReviewDto dto) => new Review
        {
            ReviewId = dto.ReviewId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            UserId = dto.UserId,
            Username = dto.Username,
            SpotId = dto.SpotId
        };

        private static ReviewDto ToDto(Review model) => new ReviewDto
        {
            ReviewId = model.ReviewId,
            Rating = model.Rating,
            Comment = model.Comment,
            UserId = model.UserId,
            SpotId = model.SpotId
        };
    }
}