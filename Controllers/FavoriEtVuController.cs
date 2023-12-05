using Microsoft.AspNetCore.Mvc;

using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriEtVuController : ControllerBase
    {
        private readonly ILogger<FavoriEtVuController> _logger;
        private readonly IFavoriEtVuService _favoriService;
        [HttpPost("{userId}/{filmId}")]
        public IActionResult AjouterAuxFavoris(int userId, int filmId, bool favori, bool vu)
        {
            // V�rification de l'existence de l'utilisateur et du film
            // et de l'authentification de l'utilisateur

            // Logique d'ajout du film aux favoris

            // R�ponse
            return Ok();
        }

        [HttpDelete("{userId}/{filmId}")]
        public IActionResult RetirerDesFavoris(int userId, int filmId)
        {
            // Impl�mentez la logique pour retirer un film des favoris
            // Retournez un r�sultat appropri�
            return Ok();
        }


        public FavoriEtVuController(ILogger<FavoriEtVuController> logger, IFavoriEtVuService favoriService)
        {
            _logger = logger;
            _favoriService = favoriService;
        }

        [HttpGet("{userId}")]
        public IActionResult ListerFavoris(int userId)
        {
            try
            {
                var favoris = _favoriService.ListerFavorisParUser(userId);
                if (favoris == null || !favoris.Any())
                {
                    return NotFound("Aucun film favori trouv� pour cet utilisateur.");
                }

                return Ok(favoris);
            }
            catch (Exception ex)
            {
                // Gestion des exceptions
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }
    }
}
