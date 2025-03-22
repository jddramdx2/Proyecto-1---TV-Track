namespace Proyecto_1_TV_Track.Models

/// <summary>
/// Representa usuario y rol 
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
