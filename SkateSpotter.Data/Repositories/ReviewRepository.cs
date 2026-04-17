using MySql.Data.MySqlClient;
using SkateSpotter.Data.Interfaces;
using SkateSpotter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateSpotter.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Review review)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetAll()
        {
            throw new NotImplementedException();
        }

        public Review? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetBySpotId(int spotId)
        {
            var list = new List<Review>();

            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(
                @"SELECT r.ReviewId, r.SpotId, r.UserId, r.Comment, r.Rating,
                 u.Username, u.Name
          FROM review r
          JOIN user u ON r.UserId = u.UserId
          WHERE r.SpotId = @id",
                conn
            );

            cmd.Parameters.AddWithValue("@id", spotId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Review
                {
                    ReviewId = reader.GetInt32("ReviewId"),
                    SpotId = reader.GetInt32("SpotId"),
                    UserId = reader.GetInt32("UserId"),
                    Name = reader.GetString("Name"),
                    Comment = reader.GetString("Comment"),
                    Rating = reader.GetInt32("Rating"),
                    Username = reader.GetString("Username")
                });
            }

            return list;
        }




        IEnumerable<Review> IReviewRepository.GetBySpotId(int spotId)
        {
            return GetBySpotId(spotId);
        }
    }
}