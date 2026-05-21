using SkateSpotter.Logic.Models;

namespace SkateSpotter.Logic.Interfaces
{
    public interface ISpotService
    {
        IEnumerable<Spot> GetAll();
        Spot? GetById(int id);
        void Create(Spot spot);
    }
}