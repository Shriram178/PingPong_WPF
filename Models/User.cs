using CsvHelper.Configuration.Attributes;

namespace BounceBall.Models
{
    public class User
    {
        public User() { }

        public User(string name, string password)
        {
            UserName = name;
            Password = password;
        }

        [Name("UserName")]
        public string UserName { get; set; }

        [Name("Password")]
        public string Password { get; set; }
    }
}
