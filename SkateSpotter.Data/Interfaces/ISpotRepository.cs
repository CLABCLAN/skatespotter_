using SkateSpotter.Data.Models;

namespace SkateSpotter.Data.Interfaces
{
    public interface ISpotRepository
    {
        IEnumerable<Spot> GetAll();
    }
}
