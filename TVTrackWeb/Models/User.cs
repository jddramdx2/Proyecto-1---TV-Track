namespace TVTrackWeb.Models // este es el namespace correcto
{
    // esta clase representa al usuario con su nombre y rol
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