using Moq;

using MovieAPI.Repositories;
using MovieAPI.Services;

using NUnit.Framework;

namespace MovieAPI.UnitTests
{
    [TestFixture]
    public class FavoriServiceTests
    {
        private Mock<IFavoriRepository> _mockFavoriRepository;
        private FavoriEtVuService _favoriEtVuService;

        [SetUp]
        public void Setup()
        {
            _mockFavoriRepository = new Mock<IFavoriRepository>();
            _favoriEtVuService = new FavoriEtVuService(_mockFavoriRepository.Object);
        }

        [Test]
        public async Task ListerFavoris_WhenCalled_ReturnsFavorisList()
        {
            // Arrange
            var userId = 2;
            var mockFavoris = new List<FavoriEtVu>
            {
                new FavoriEtVu { Favori=true, FilmId=1, UserId=userId, Vu=true },
                new FavoriEtVu { Favori=false, FilmId=2, UserId=userId, Vu=false },
          

            };
            _mockFavoriRepository.Setup(repo => repo.LireFavorisDepuisJson(It.IsAny<string>()))
                                 .Returns(mockFavoris);

            // Act
            var result = _favoriEtVuService.ListerFavorisParUser(userId);

            // Assert
            Assert.AreEqual(mockFavoris, result);
        }


    }
}
