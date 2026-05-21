using SkateSpotter.Logic.Models;

namespace SkateSpotter.Logic.Interfaces
{
    public interface IUserService
    {
        User? Login(string username, string password);
        void Register(string username, string password, string? name);
    }
}