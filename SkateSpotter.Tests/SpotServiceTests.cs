using Moq;
using SkateSpotter.Logic.Interfaces;
using SkateSpotter.Logic.Models;
using SkateSpotter.Logic.Services;

namespace SkateSpotter.Tests
{
    public class SpotServiceTests
    {
        private readonly Mock<ISpotRepository> _mockRepository;
        private readonly SpotService _spotService;

        public SpotServiceTests()
        {
            _mockRepository = new Mock<ISpotRepository>();
            _spotService = new SpotService(_mockRepository.Object);
        }

        // TC-001.1 — Spot succesvol opslaan
        [Fact]
        public void Create_GeldigeSpot_WordtOpgeslagen()
        {
            // Arrange
            var spot = new Spot
            {
                Name = "Vondelpark",
                Latitude = 52.3580m,
                Longitude = 4.8686m,
                CreatedUserId = 1
            };

            // Act
            _spotService.Create(spot);

            // Assert
            _mockRepository.Verify(r => r.Create(spot), Times.Once);
        }

        // TC-001.2 — Naam ontbreekt
        [Fact]
        public void Create_NaamOntbreekt_GooitArgumentException()
        {
            // Arrange
            var spot = new Spot
            {
                Name = "",
                Latitude = 52.3580m,
                Longitude = 4.8686m,
                CreatedUserId = 1
            };

            // Act
            var exception = Assert.Throws<ArgumentException>(() => _spotService.Create(spot));

            // Assert
            Assert.Equal("Naam is verplicht.", exception.Message);
            _mockRepository.Verify(r => r.Create(It.IsAny<Spot>()), Times.Never);
        }

        // TC-001.3 — Ongeldige naam (speciale tekens of meer dan 20 tekens)
        [Theory]
        [InlineData("Spot@Centrum!")]
        [InlineData("DezeNaamIsVeelEnVeelTeLang")]
        [InlineData("Spot#1")]
        public void Create_OngeldigenNaam_GooitArgumentException(string naam)
        {
            // Arrange
            var spot = new Spot
            {
                Name = naam,
                Latitude = 52.3580m,
                Longitude = 4.8686m,
                CreatedUserId = 1
            };

            // Act
            var exception = Assert.Throws<ArgumentException>(() => _spotService.Create(spot));

            // Assert
            Assert.Equal("Ongeldige naam: geen speciale tekens en maximaal 20 tekens.", exception.Message);
            _mockRepository.Verify(r => r.Create(It.IsAny<Spot>()), Times.Never);
        }

        // TC-001.4 — Locatie ontbreekt (beide 0)
        [Fact]
        public void Create_LocatieOntbreekt_GooitArgumentException()
        {
            // Arrange
            var spot = new Spot
            {
                Name = "Vondelpark",
                Latitude = 0,
                Longitude = 0,
                CreatedUserId = 1
            };

            // Act
            var exception = Assert.Throws<ArgumentException>(() => _spotService.Create(spot));

            // Assert
            Assert.Equal("Locatie ontbreekt.", exception.Message);
            _mockRepository.Verify(r => r.Create(It.IsAny<Spot>()), Times.Never);
        }

        // TC-001.5 — Ongeldige locatie (buiten bereik)
        [Theory]
        [InlineData(91, 0)]
        [InlineData(-91, 0)]
        [InlineData(0, 181)]
        [InlineData(0, -181)]
        public void Create_OngeldigeLocatie_GooitArgumentException(decimal lat, decimal lng)
        {
            // Arrange
            var spot = new Spot
            {
                Name = "Vondelpark",
                Latitude = lat,
                Longitude = lng,
                CreatedUserId = 1
            };

            // Act
            var exception = Assert.Throws<ArgumentException>(() => _spotService.Create(spot));

            // Assert
            Assert.Equal("Ongeldige locatie.", exception.Message);
            _mockRepository.Verify(r => r.Create(It.IsAny<Spot>()), Times.Never);
        }

        // TC-001.6 — Database fout
        [Fact]
        public void Create_DatabaseFout_GooitException()
        {
            // Arrange
            var spot = new Spot
            {
                Name = "Vondelpark",
                Latitude = 52.3580m,
                Longitude = 4.8686m,
                CreatedUserId = 1
            };

            _mockRepository
                .Setup(r => r.Create(It.IsAny<Spot>()))
                .Throws(new Exception("Database onbereikbaar"));

            // Act
            var exception = Assert.Throws<Exception>(() => _spotService.Create(spot));

            // Assert
            Assert.Equal("Database onbereikbaar", exception.Message);
        }
    }
}