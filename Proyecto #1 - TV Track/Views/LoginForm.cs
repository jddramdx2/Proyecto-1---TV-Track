using System;
using System.Linq;
using System.Windows.Forms;
using Proyecto__1___TV_Track.Views;
using Proyecto_1_TV_Track.Data;
using Proyecto_1_TV_Track.Models;

namespace Proyecto_1_TV_Track.Views
{
    public partial class LoginForm : Form
    {
        private UserRepository userRepo; // Cargo la lista de los usuarios del archivo CSV

        public LoginForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
            LoadRoles(); // Carga roles disponibles

            // Hace coneccion de los botones a sus respectivas funciones
            btnLogin.Click += btnLogin_Click;
            btnRegister.Click += btnRegister_Click;
        }

        /// <summary>
        /// Agrega los roles disponibles al combo box.
        /// </summary>
        private void LoadRoles()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("User");
            cmbRole.SelectedIndex = 0;// Esto es con el fin de que tenga valor y no este vació
        }

        /// <summary>
        /// Verifica la existencia del usuario y deja que este inicié de sesión.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Ingrese su nombre y seleccione un rol, por favor.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var users = userRepo.GetUsers(); // realiza una busqueda de usuarios en el CSV

                // Mostrar temporalmente todos los usuarios cargados (puedes eliminar esto después de probar)
                // MessageBox.Show(string.Join("\n", users.Select(u => $"Nombre: '{u.Name}' - Rol: '{u.Role}'")), "DEBUG CSV");

                var user = users.FirstOrDefault(u =>
                    string.Equals(u.Name.Trim(), username.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(u.Role.Trim(), role.Trim(), StringComparison.OrdinalIgnoreCase));

                if (user != null) // Verifica que el usuario se encuetre la lista y confirma
                {
                    MessageBox.Show($"Bienvenido, {user.Name}!", "INGRESO EXITOSO :)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide(); // se esconde la ventana de login
                    if (user.Role.Trim() == "Admin")
                    {
                        // Se abre erl formulario de opciones del administrador
                        AdminChoiceForm choiceForm = new AdminChoiceForm();

                        if (choiceForm.ShowDialog() == DialogResult.OK)
                        {
                            if (choiceForm.SelectedOption == AdminChoiceForm.AdminOption.MovieCatalog)
                            {
                                MovieForm movieForm = new MovieForm();
                                movieForm.ShowDialog();
                            }
                            else if (choiceForm.SelectedOption == AdminChoiceForm.AdminOption.Reports)
                            {
                                AdminReportForm adminForm = new AdminReportForm();
                                adminForm.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        // Si el usuario no es administrador, solo se abrira el catalogo de películas
                        MovieForm movieForm = new MovieForm();
                        movieForm.ShowDialog();
                    }

                    // Una vez que el usuario cierre su sesion, se mostrara el login de nuevo
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Oh oh, ¿existes?", "Error de ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hubo un problema al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Se abrira una ventana para hacer el registro de un nuevo usuario
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                UserRegisterForm registerForm = new UserRegisterForm(); // Abre pantalla de registro
                registerForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hubo un problema al abrir el formulario de registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Este método es para cuando se hace clic en la etiqueta, pero no hace nada.
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {
            // Se colocó para evitar errores en el sistema
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }
    }
}