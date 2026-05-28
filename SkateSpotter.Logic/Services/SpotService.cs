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

            if (!System.Text.RegularExpressions.Regex.IsMatch(spot.Name, @"^[a-zA-Z0-9\s]+$") || spot.Name.Length > 20)
                throw new ArgumentException("Ongeldige naam: geen speciale tekens en maximaal 20 tekens.");

            if (spot.Latitude == 0 && spot.Longitude == 0)
                throw new ArgumentException("Locatie ontbreekt.");

            if (spot.Latitude < -90 || spot.Latitude > 90 || spot.Longitude < -180 || spot.Longitude > 180)
                throw new ArgumentException("Ongeldige locatie.");

            _spotRepository.Create(spot);
        }
    }
}