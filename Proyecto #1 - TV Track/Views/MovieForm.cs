using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_1_TV_Track.Data;
using Proyecto_1_TV_Track.Models;
using Proyecto_1_TV_Track.Views;

namespace Proyecto__1___TV_Track.Views
{
    public partial class MovieForm : Form
    {
        private MovieRepository movieRepo = new MovieRepository();
        private List<Movie> movies = new List<Movie>();

        public MovieForm()
        {
            InitializeComponent();
        }

        private void MovieForm_Load(object sender, EventArgs e)
        {
            LoadMovies();
        }

        private void LoadMovies()
        {
            movies = movieRepo.GetMovies();
            dgvMovies.DataSource = movies.Select(m => new { m.Title, m.Genre, m.Platform }).ToList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchQuery))
            {
                LoadMovies();
            }
            else
            {
                var filteredMovies = movies
                    .Where(m => m.Title.ToLower().Contains(searchQuery) ||
                                m.Genre.ToLower().Contains(searchQuery) ||
                                m.Platform.ToLower().Contains(searchQuery))
                    .Select(m => new { m.Title, m.Genre, m.Platform })
                    .ToList();

                dgvMovies.DataSource = filteredMovies;
            }
        }

        /// <summary>
        /// Handles logout button click - returns to LoginForm and closes the session.
        /// </summary>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Ensure the login form is displayed before closing the current form
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            // Close the application after login form is handled
            Application.Exit();
        }

        private void Exit()
        {
            throw new NotImplementedException();
        }
    }
}