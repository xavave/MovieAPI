namespace MovieAPI.Repositories
{
    public class FavoriRepository : IFavoriRepository
    {
        public IEnumerable<Film> GetFavorisByUserId(int userId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<FavoriEtVu> LireFavorisDepuisJson(string cheminFichier)
        {
            var json = File.ReadAllText(cheminFichier);
            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<FavoriEtVu>>(json);
        }

    }

    public interface IFavoriRepository
    {
        IEnumerable<Film> GetFavorisByUserId(int userId);
        IEnumerable<FavoriEtVu> LireFavorisDepuisJson(string cheminFichier);
    }
}
