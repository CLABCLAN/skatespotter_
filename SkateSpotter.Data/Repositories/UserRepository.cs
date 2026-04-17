using MySql.Data.MySqlClient;
using SkateSpotter.Data.Interfaces;
using SkateSpotter.Data.Models;

namespace SkateSpotter.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User? GetById(int userId)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(
                "SELECT UserId, Username, Name FROM user WHERE UserId = @id",
                conn
            );

            cmd.Parameters.AddWithValue("@id", userId);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    UserId = reader.GetInt32("UserId"),
                    Username = reader.GetString("Username"),
                    Name = reader.GetString("Name")
                };
            }

            return null;
        }
    }
}
