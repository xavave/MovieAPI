using Microsoft.AspNetCore.Mvc;

using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        [HttpPost()]
        public IActionResult AjouterUser(string nom, string email)
        {

            // Logique d'ajout du film 
            try
            {
                var user = new Utilisateur() { Nom = nom, Email = email };
                _userService.AjouterUser(user);

                return Ok(user);
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
        public IActionResult RetirerUser(int userId)
        {
            try
            {
                _userService.RetirerUser(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Gestion des exceptions
                return StatusCode(500, $"Une erreur interne est survenue:{ex}");
            }
        }


        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _userService = service;
        }

        [HttpGet()]
        public IActionResult ListerUsers()
        {
            try
            {
                var users = _userService.ListerUsers();
                if (users == null || !users.Any())
                {
                    return NotFound("Aucun utilisateur trouvé.");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                // Gestion des exceptions
                return StatusCode(500, $"Une erreur interne est survenue:{ex}");
            }
        }
    }
}
