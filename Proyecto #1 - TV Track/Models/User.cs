namespace Proyecto_1_TV_Track.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Role { get; set; }

        public User(string name, string role)
        {
            Name = name;
            Role = role;
        }
    }
}
