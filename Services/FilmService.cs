using System.Text.Json;

using Newtonsoft.Json;
namespace MovieAPI.Services
{
    public interface IFilmService
    {
        IList<Film> ListerFilms();

        Film GetFilm(int filmId);
        void AjouterFilm(Film film);
        void RetirerFilm(int filmId);
    }
    public class FilmService : IFilmService
    {
        public string filmsJsonPath = ".\\Data\\Films.Json";
        public IList<Film> ListerFilms()
        {
            return LireFilmsDepuisJson(filmsJsonPath).ToList();
        }

        private IEnumerable<Film> LireFilmsDepuisJson(string cheminFichier)
        {
            var json = File.ReadAllText(cheminFichier);
            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Film>>(json);
        }
        public Film GetFilm(int filmId)
        {
            var json = File.ReadAllText(filmsJsonPath);
            return LireFilmsDepuisJson(filmsJsonPath).Where(f => f.Id == filmId).FirstOrDefault();
        }
        public void AjouterFilm(Film film)
        {
            var films = ListerFilms();
            film.Id = films.Count();
            films.Add(film);
            var jsonData = JsonConvert.SerializeObject(films);
            System.IO.File.WriteAllText(filmsJsonPath, jsonData);
        }
        public void RetirerFilm(int filmId)
        {
            var films = ListerFilms();
            var film = films.FirstOrDefault(f => f.Id == filmId);
            if (film == null) return;
            films.Remove(film);
            var jsonData = JsonConvert.SerializeObject(films);
            System.IO.File.WriteAllText(filmsJsonPath, jsonData);
        }
    }
}
