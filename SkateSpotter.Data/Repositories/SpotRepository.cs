using MySql.Data.MySqlClient;
using SkateSpotter.Data.Interfaces;
using SkateSpotter.Data.Models;

namespace SkateSpotter.Data.Repositories
{
    public class SpotRepository : ISpotRepository
    {
        private readonly string _connectionString;

        public SpotRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Spot> GetAll()
        {
            var spots = new List<Spot>();

            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand("SELECT SpotId, Name, Latitude, Longitude FROM spot", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                spots.Add(new Spot
                {
                    SpotId = reader.GetInt32("SpotId"),
                    Name = reader.GetString("Name"),
                    Latitude = reader.GetDecimal("Latitude"),
                    Longitude = reader.GetDecimal("Longitude")
                });
            }

            return spots;
        }
    }
}
