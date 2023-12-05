using Microsoft.AspNetCore.Mvc;

using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly ILogger<FilmController> _logger;
        private readonly IFilmService _filmService;
        [HttpPost()]
        public IActionResult AjouterFilm(string title, DateOnly dateSortie, double noteGlobale)
        {

            // Logique d'ajout du film 
            try
            {
                var film = new Film() { DateSortie = dateSortie, Titre = title, NoteGlobale = noteGlobale };
                _filmService.AjouterFilm(film);

                return Ok(film);
            }
            catch (Exception ex)
            {
                // Gestion des exceptions
                return StatusCode(500, $"Une erreur interne est survenue:{ex}");
            }
            // Réponse
            return Ok();
        }

        [HttpDelete("{filmId}")]
        public IActionResult RetirerFilm(int filmId)
        {
            try
            {
                _filmService.RetirerFilm(filmId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Gestion des exceptions
                return StatusCode(500, $"Une erreur interne est survenue:{ex}");
            }
        }


        public FilmController(ILogger<FilmController> logger, IFilmService service)
        {
            _logger = logger;
            _filmService = service;
        }

        [HttpGet()]
        public IActionResult ListerFilms()
        {
            try
            {
                var films = _filmService.ListerFilms();
                if (films == null || !films.Any())
                {
                    return NotFound("Aucun film trouvé.");
                }

                return Ok(films);
            }
            catch (Exception ex)
            {
                // Gestion des exceptions
                return StatusCode(500, $"Une erreur interne est survenue:{ex}");
            }
        }
    }
}
