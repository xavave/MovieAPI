using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

using MovieAPI.Repositories;

using Newtonsoft.Json;
namespace MovieAPI.Services
{

    public interface IFavoriEtVuService
    {
        IEnumerable<FavoriEtVu> ListerFavorisParUser(int userId);
        void AjouterOuMajFavori(int userId, int filmId, bool isFavori, bool isVu);
        void RetirerFavori(int userId, int filmId);

    }

    public class FavoriEtVuService : IFavoriEtVuService
    {
        private readonly IFavoriRepository _favoriRepository;
        public FavoriEtVuService(IFavoriRepository favoriRepository)
        {
            _favoriRepository = favoriRepository;
        }
        private string favorisJsonPath = ".\\Data\\FavorisEtVus.Json";
        public IEnumerable<FavoriEtVu> ListerFavorisParUser(int userId)
        {
            return _favoriRepository.LireFavorisDepuisJson(favorisJsonPath)
                .Where(f => f.UserId == userId)
                .Select(f => f)
                .ToList();
        }
       
        public void AjouterOuMajFavori(int userId, int filmId, bool isFavori, bool isVu)
        {
            var favoris = _favoriRepository.LireFavorisDepuisJson(favorisJsonPath).ToList();
            var favo = favoris.FirstOrDefault(f => f.UserId == userId && f.FilmId == filmId);
            if (favo == null) favo = new FavoriEtVu() { UserId = userId, FilmId = filmId, Favori = isFavori, Vu = isVu };
            favoris.Add(favo);
            var jsonData = JsonConvert.SerializeObject(favoris);
            System.IO.File.WriteAllText(favorisJsonPath, jsonData);
        }

        public void RetirerFavori(int userId, int filmId)
        {
            var favoris = _favoriRepository.LireFavorisDepuisJson(favorisJsonPath).ToList();
            var favo = favoris.FirstOrDefault(f => f.UserId == userId && f.FilmId == filmId);
            if (favo == null) return;
            favoris.Remove(favo);
            var jsonData = JsonConvert.SerializeObject(favoris);
            System.IO.File.WriteAllText(favorisJsonPath, jsonData);
        }
    }


}
