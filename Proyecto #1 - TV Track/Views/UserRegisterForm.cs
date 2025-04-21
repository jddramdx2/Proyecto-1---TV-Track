using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_1_TV_Track.Views
{
    public partial class UserRegisterForm : Form
    {
        private string userFilePath = "Lista_100_usuarios.csv"; // Path to user CSV file

        public UserRegisterForm()
        {
            InitializeComponent();
            LoadRoles(); // Populate dropdown
        }

        /// <summary>
        /// Populates the role dropdown with available roles.
        /// </summary>
        private void LoadRoles()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("User");
            cmbRole.SelectedIndex = 0; // Set default selection
        }

        /// <summary>
        /// Handles Register button click - Saves user to CSV.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please enter a username and select a role.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (UserExists(username))
            {
                MessageBox.Show("This username already exists. Please choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Ensure we use the correct full file path
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lista_100_usuarios.csv");

                // Append the new user with correct format
                File.AppendAllText(fullPath, $"{username},{role}{Environment.NewLine}");

                MessageBox.Show($"User {username} registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Close after successful registration
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Checks if the username already exists in the CSV file.
        /// </summary>
        private bool UserExists(string username)
        {
            if (!File.Exists(userFilePath)) return false;

            var existingUsers = File.ReadAllLines(userFilePath)
                                    .Select(line => line.Split(','))
                                    .Where(parts => parts.Length > 0)
                                    .Select(parts => parts[0].Trim().ToLower());

            return existingUsers.Contains(username.ToLower());
        }

        /// <summary>
        /// Handles Cancel button click - Closes the form.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
