namespace BounceBall.Models
{
    public class User
    {
        public User(string name, string password)
        {
            Id = Guid.NewGuid();
            UserName = name;
            Password = password;
        }

        public Guid Id { get; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
