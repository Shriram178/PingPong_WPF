using CsvHelper.Configuration.Attributes;

namespace BounceBall.Models
{
    public partial class User
    {
        public User() { }

        public User(string name, string password, string eamil)
        {
            UserName = name;
            Password = password;
            Email = eamil;
        }

        [Name("UserName")]
        public string UserName { get; set; }

        [Name("Password")]
        public string Password { get; set; }

        public string Email { get; set; }

    }
}
