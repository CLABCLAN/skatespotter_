using SkateSpotter.Logic.Interfaces;
using SkateSpotter.Logic.Models;

namespace SkateSpotter.Logic.Services
{
    public class SpotService : ISpotService
    {
        private readonly ISpotRepository _spotRepository;

        public SpotService(ISpotRepository spotRepository)
        {
            _spotRepository = spotRepository;
        }

        public IEnumerable<Spot> GetAll() => _spotRepository.GetAll();

        public Spot? GetById(int id) => _spotRepository.GetById(id);

        public void Create(Spot spot)
        {
            if (string.IsNullOrWhiteSpace(spot.Name))
                throw new ArgumentException("Naam is verplicht.");

            _spotRepository.Create(spot);
        }
    }
}