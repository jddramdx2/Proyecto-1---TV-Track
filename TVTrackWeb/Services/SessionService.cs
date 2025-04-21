namespace TVTrackWeb.Services
{
    public class SesionService
    {
        public string Nombre { get; private set; }
        public string Rol { get; private set; }
        public bool EstaLogueado { get; private set; }

        // Método para guardar sesión
        public void IniciarSesion(string nombre, string rol)
        {
            Nombre = nombre;
            Rol = rol;
            EstaLogueado = true;
        }

        // Método para cerrar sesión
        public void CerrarSesion()
        {
            Nombre = string.Empty;
            Rol = string.Empty;
            EstaLogueado = false;
        }
    }
}
