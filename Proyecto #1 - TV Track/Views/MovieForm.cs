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
            dgvMovies.DataSource = movies.Select(m => new
            {
                m.Title,
                m.Genre,
                m.Platform,
                m.Rating,
                m.ViewStatus,
                Recomendado = m.IsRecommended ? "Sí" : "No" // columna de recomendaciones
            }).ToList();
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
                    .Select(m => new
                    {
                        m.Title,
                        m.Genre,
                        m.Platform,
                        m.Rating,
                        m.ViewStatus,
                        Recomendado = m.IsRecommended ? "Sí" : "No"
                    })
                    .ToList();

                dgvMovies.DataSource = filteredMovies;
            }
        }

        /// <summary>
        /// Cuando se hace clic en el botón para salir, se vuelve a la pantalla de inicio de sesión y se cierra la sesión..
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

        /// <summary>
        /// Se puede darle una calificación a la película que uno escoja.
        /// </summary>
        private void btnRate_Click(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una película para calificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbRating.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una calificación antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedMovie = dgvMovies.SelectedRows[0].Cells[0].Value.ToString(); // Título de la peli
            int rating = int.Parse(cmbRating.SelectedItem.ToString()); // Calificación

            movieRepo.RateMovie(selectedMovie, rating); // Guarda la calificación en el CSV

            MessageBox.Show($"Has calificado '{selectedMovie}' con {rating} estrellas.", "Calificación guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadMovies(); // Recargar la lista con la nueva calificación
        }

        /// <summary>
        /// Permite cambiar el estado de visualización de la pelicula.
        /// </summary>
        private void btnMarkViewStatus_Click(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una película para marcar su estado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedMovie = dgvMovies.SelectedRows[0].Cells[0].Value.ToString(); // Obtiene el título de la película

            // Muestra opciones para el usuario
            DialogResult result = MessageBox.Show(
                "¿Cómo desea marcar esta película?\n\nSí → Visto\nNo → Parcialmente Visto",
                "Marcar estado de película",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            string viewStatus = "No Visto"; // Por defecto

            if (result == DialogResult.Yes)
            {
                viewStatus = "Visto";
            }
            else if (result == DialogResult.No)
            {
                viewStatus = "Parcialmente Visto";
            }

            // 📌 Si el usuario no cancela, actualiza el estado en el CSV
            if (result != DialogResult.Cancel)
            {
                movieRepo.UpdateMovieViewStatus(selectedMovie, viewStatus);
                LoadMovies(); // 📌 Recargar lista para reflejar cambios
                MessageBox.Show($"Se ha marcado '{selectedMovie}' como '{viewStatus}'.", "Estado actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Permite filtrar películas por estado de visualización.
        /// </summary>
        private void btnFilterViewStatus_Click(object sender, EventArgs e)
        {
            if (cmbViewStatus.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un estado de vista para filtrar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedStatus = cmbViewStatus.SelectedItem.ToString();

            var filteredMovies = movies
                .Where(m => m.ViewStatus == selectedStatus)
                .Select(m => new
                {
                    m.Title,
                    m.Genre,
                    m.Platform,
                    m.Rating,
                    m.ViewStatus,
                    Recomendado = m.IsRecommended ? "Sí" : "No"
                })
                .ToList();

            dgvMovies.DataSource = filteredMovies;
        }

        /// <summary>
        /// Filtra solo las películas recomendadas.
        /// </summary>
        private void btnFilterRecommended_Click(object sender, EventArgs e)
        {
            var recommendedMovies = movies
                .Where(m => m.IsRecommended)
                .Select(m => new
                {
                    m.Title,
                    m.Genre,
                    m.Platform,
                    m.Rating,
                    m.ViewStatus,
                    Recomendado = "Sí"
                })
                .ToList();

            dgvMovies.DataSource = recommendedMovies;
        }
    }
}