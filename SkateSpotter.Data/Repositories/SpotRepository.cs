using Dapper;
using MySql.Data.MySqlClient;
using SkateSpotter.Logic.Interfaces;
using SkateSpotter.Logic.Models;
using SkateSpotter.Data.DTOs;

namespace SkateSpotter.Data.Repositories
{
    public class SpotRepository : ISpotRepository
    {
        private readonly string _connectionString;

        public SpotRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection CreateConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Spot> GetAll()
        {
            using var conn = CreateConnection();

            const string sql = @"
SELECT 
    s.SpotId, s.Name, s.Latitude, s.Longitude, s.CreatedUserId,
    u.Username AS CreatedByUsername,
    r.ReviewId, r.SpotId, r.Rating, r.Comment
FROM spot s
LEFT JOIN user u ON s.CreatedUserId = u.UserId
LEFT JOIN review r ON s.SpotId = r.SpotId";

            var dict = new Dictionary<int, SpotDto>();

            conn.Query<SpotDto, ReviewDto, SpotDto>(
                sql,
                (spot, review) =>
                {
                    if (!dict.TryGetValue(spot.SpotId, out var entry))
                    {
                        entry = spot;
                        entry.Reviews = new List<ReviewDto>();
                        dict.Add(entry.SpotId, entry);
                    }

                    if (review != null)
                        entry.Reviews.Add(review);

                    return entry;
                },
                splitOn: "ReviewId"
            );

            return dict.Values.Select(ToModel);
        }


        public Spot? GetById(int id)
        {
            using var conn = CreateConnection();
            const string sql = @"
        SELECT s.*, u.Username as CreatedByUsername
        FROM spot s
        LEFT JOIN user u ON s.CreatedUserId = u.UserId
        WHERE s.SpotId = @Id";

            var dto = conn.QueryFirstOrDefault<SpotDto>(sql, new { Id = id });
            return dto == null ? null : ToModel(dto);
        }

        public void Create(Spot spot)
        {
            using var conn = CreateConnection();
            const string sql = @"
                INSERT INTO spot (Name, Latitude, Longitude, CreatedUserId)
                VALUES (@Name, @Latitude, @Longitude, @CreatedUserId)";

            conn.Execute(sql, ToDto(spot));
        }

        // Mapping
        private Spot ToModel(SpotDto dto)
        {
            return new Spot
            {
                SpotId = dto.SpotId,
                Name = dto.Name,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                CreatedUserId = dto.CreatedUserId,
                CreatedByUsername = dto.CreatedByUsername,
                Reviews = dto.Reviews.Select(r => new Review
                {
                    ReviewId = r.ReviewId,
                    SpotId = r.SpotId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                }).ToList()
            };
        }


        private static SpotDto ToDto(Spot model) => new SpotDto
        {
            SpotId = model.SpotId,
            Name = model.Name,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            CreatedUserId = model.CreatedUserId
        };
    }
}