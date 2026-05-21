using SkateSpotter.Logic.Models;

namespace SkateSpotter.Logic.Interfaces
{
    public interface IUserRepository
    {
        User? GetById(int id);
        User? GetByUsername(string username);
        void Create(User user);
    }
}