using BounceBall.Models;

namespace BounceBall.Manager
{
    public class UserManager
    {
        private List<User> users = new List<User>();

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public bool ValidateUser(string username, string password)
        {
            return users.Any(u => u.UserName == username && u.Password == password);
        }

        public User GetUser(string userName, string password)
        {
            return users.First(u => u.UserName == userName && u.Password == password);
        }
    }

}
