using System;
using System.Linq;
using System.Windows.Forms;
using Proyecto_1_TV_Track.Data;
using Proyecto_1_TV_Track.Models;

namespace Proyecto_1_TV_Track.Views
{
    public partial class LoginForm : Form
    {
        private UserRepository userRepo; // Manages users from CSV

        public LoginForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
            LoadRoles(); // Populate role dropdown

            // Attach event handlers
            btnLogin.Click += btnLogin_Click;
            btnRegister.Click += btnRegister_Click;
        }

        /// <summary>
        /// Loads available user roles into the dropdown.
        /// </summary>
        private void LoadRoles()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("User");
            cmbRole.SelectedIndex = 0; // Default selection to avoid null issues
        }

        /// <summary>
        /// Handles user login validation.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please enter your username and select a role.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var users = userRepo.GetUsers(); // Get users from CSV
                var user = users.FirstOrDefault(u =>
                    string.Equals(u.Name, username, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(u.Role, role, StringComparison.OrdinalIgnoreCase));

                if (user != null)
                {
                    MessageBox.Show($"Welcome, {user.Name}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide(); // Hide login form

                    // TODO: Open the main application form if applicable
                    // MainForm mainForm = new MainForm();
                    // mainForm.Show();
                }
                else
                {
                    MessageBox.Show("User not found. Please check your credentials.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while logging in: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens the user registration form when "Register" is clicked.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                UserRegisterForm registerForm = new UserRegisterForm(); // Updated reference
                registerForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the registration form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Fix for CS1061 error: Adds missing label click event handler.
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {
            // No logic needed, just prevents missing method error
        }
    }
}
