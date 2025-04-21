namespace Proyecto_1_TV_Track.Models

/// <summary>
/// Muestra el usuario y su respectivo rol
/// </summary>

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
