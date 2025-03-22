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
        private UserRepository userRepo; // Maneja los usuarios del archivo CSV

        public LoginForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
            LoadRoles(); // Carga los roles disponibles

            // Conecta los botones con sus respectivas funciones
            btnLogin.Click += btnLogin_Click;
            btnRegister.Click += btnRegister_Click;
        }

   
        /// Agrega los roles disponibles al combo box.
    
        private void LoadRoles()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("User");
            cmbRole.SelectedIndex = 0; // Para que siempre tenga un valor y no quede vacío
        }

     
        /// Verifica si el usuario existe y permite el inicio de sesión.
       
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
                var users = userRepo.GetUsers(); // Busca usuarios en el CSV
                var user = users.FirstOrDefault(u =>
                    string.Equals(u.Name, username, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(u.Role, role, StringComparison.OrdinalIgnoreCase));

                if (user != null)  // verifica el usuario en la lista y confirma
                {
                    MessageBox.Show($"Bienvenido, {user.Name}!", "INGRESO EXITOSO :)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide(); 

                    MovieForm movieForm = new MovieForm();
                    movieForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("oh oh, existes?", "Error de ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hubo un problema al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Abre la ventana para registrar un nuevo usuario.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                UserRegisterForm registerForm = new UserRegisterForm(); // Se abre la pantalla de registro
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
            // Solo está aquí para evitar errores del sistema
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }
    }
}