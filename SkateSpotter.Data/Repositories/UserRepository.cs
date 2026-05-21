using Dapper;
using MySql.Data.MySqlClient;
using SkateSpotter.Data.DTOs;
using SkateSpotter.Logic.Interfaces;
using SkateSpotter.Logic.Models;

namespace SkateSpotter.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection CreateConnection() => new MySqlConnection(_connectionString);

        public User? GetById(int id)
        {
            using var conn = CreateConnection();
            var dto = conn.QueryFirstOrDefault<UserDto>(
                "SELECT * FROM user WHERE UserId = @Id", new { Id = id });
            return dto == null ? null : ToModel(dto);
        }

        public User? GetByUsername(string username)
        {
            using var conn = CreateConnection();
            var dto = conn.QueryFirstOrDefault<UserDto>(
                "SELECT * FROM user WHERE Username = @Username", new { Username = username });
            return dto == null ? null : ToModel(dto);
        }

        public void Create(User user)
        {
            using var conn = CreateConnection();
            const string sql = @"
                INSERT INTO user (Username, PasswordHashed, Name)
                VALUES (@Username, @PasswordHashed, @Name)";
            conn.Execute(sql, ToDto(user));
        }

        private static User ToModel(UserDto dto) => new User
        {
            UserId = dto.UserId,
            Username = dto.Username,
            PasswordHashed = dto.PasswordHashed,
            Name = dto.Name
        };

        private static UserDto ToDto(User model) => new UserDto
        {
            UserId = model.UserId,
            Username = model.Username,
            PasswordHashed = model.PasswordHashed,
            Name = model.Name
        };
    }
}