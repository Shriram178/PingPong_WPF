using BounceBall.Models;

namespace BounceBall.Manager
{
    public class UserManager
    {
        private readonly FileHandler _fileHandler;
        private List<User> _users = new List<User>();
        public User CurrentUser { get; set; }

        public UserManager(FileHandler fileHandler)
        {
            _fileHandler = fileHandler;
            _users = _fileHandler.LoadUsers();
        }

        public void AddUser(User user)
        {
            if (_users.Any(u => u.UserName == user.UserName))
                throw new InvalidOperationException($"User with the username {user.UserName} already exists.");

            _users.Add(user);
            _fileHandler.SaveUsers(_users);
        }

        public bool ValidateUser(string username, string password)
        {
            return _users.Any(u => u.UserName == username && u.Password == password);
        }

        public User GetUser(string userName, string password)
        {
            return _users.First(u => u.UserName == userName && u.Password == password);
        }


        public void Logout()
        {
            CurrentUser = null;
        }
    }

}
