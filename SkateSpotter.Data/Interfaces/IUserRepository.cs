using SkateSpotter.Data.Models;

namespace SkateSpotter.Data.Interfaces
{
    public interface IUserRepository
    {
        User? GetById(int userId);
    }
}
