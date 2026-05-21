using SkateSpotter.Logic.Interfaces;
using SkateSpotter.Logic.Models;

namespace SkateSpotter.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username en wachtwoord zijn verplicht.");

            var user = _userRepository.GetByUsername(username);
            if (user == null) return null;

            bool correct = BCrypt.Net.BCrypt.Verify(password, user.PasswordHashed);
            return correct ? user : null;
        }

        public void Register(string username, string password, string? name)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is verplicht.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                throw new ArgumentException("Wachtwoord moet minimaal 6 tekens zijn.");

            if (_userRepository.GetByUsername(username) != null)
                throw new ArgumentException("Username is al in gebruik.");

            var user = new User
            {
                Username = username,
                PasswordHashed = BCrypt.Net.BCrypt.HashPassword(password),
                Name = name
            };

            _userRepository.Create(user);
        }
    }
}