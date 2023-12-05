using System.Text.Json;

using Newtonsoft.Json;
namespace MovieAPI.Services
{
    public interface IUserService
    {
        IList<Utilisateur> ListerUsers();

        Utilisateur GetUser(int userId);
        void AjouterUser(Utilisateur user);
        void RetirerUser(int userId);
    }
    public class UserService : IUserService
    {
        public string usersJsonPath = ".\\Data\\Users.Json";
        public IList<Utilisateur> ListerUsers()
        {
            return LireUsersDepuisJson(usersJsonPath).ToList();
        }

        private IEnumerable<Utilisateur> LireUsersDepuisJson(string cheminFichier)
        {
            var json = File.ReadAllText(cheminFichier);
            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Utilisateur>>(json);
        }
        public Utilisateur GetUser(int userId)
        {
            var json = File.ReadAllText(usersJsonPath);
            return LireUsersDepuisJson(usersJsonPath).Where(f => f.Id == userId).FirstOrDefault();
        }
        public void AjouterUser(Utilisateur user)
        {
            var users = ListerUsers();
            user.Id = users.Count();
            users.Add(user);
            var jsonData = JsonConvert.SerializeObject(users);
            System.IO.File.WriteAllText(usersJsonPath, jsonData);
        }
        public void RetirerUser(int userId)
        {
            var users = ListerUsers();
            var film = users.FirstOrDefault(f => f.Id == userId);
            if (film == null) return;
            users.Remove(film);
            var jsonData = JsonConvert.SerializeObject(users);
            System.IO.File.WriteAllText(usersJsonPath, jsonData);
        }
    }
}
