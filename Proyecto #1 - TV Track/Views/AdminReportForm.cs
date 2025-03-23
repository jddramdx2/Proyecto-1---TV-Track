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

namespace Proyecto__1___TV_Track.Views
{
    public partial class AdminReportForm : Form
    {
        private MovieRepository movieRepo = new MovieRepository(); // 📌 Se conecta con las películas

        public AdminReportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Cuando el formulario se abre, se cargan los reportes.
        /// </summary>
        private void AdminReportForm_Load(object sender, EventArgs e)
        {
            LoadReports(); // Llama a la función para cargar datos
        }

        /// <summary>
        /// Carga los reportes en la tabla.
        /// </summary>
        private void LoadReports()
        {
            try
            {
                List<Movie> movies = movieRepo.GetMovies(); // Obtiene todas las películas

                if (movies == null || movies.Count == 0)
                {
                    MessageBox.Show("No hay películas registradas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var groupedMovies = movies
                    .GroupBy(m => m.Genre)
                    .Select(g => new { Género = g.Key, Cantidad = g.Count() }) // Agrupa por género
                    .OrderByDescending(g => g.Cantidad)
                    .ToList();

                dgvReports.DataSource = groupedMovies; // Muestra los datos en la tabla
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los reportes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cierra la ventana y regresa al login.
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}