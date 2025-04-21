using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private MovieRepository movieRepo = new MovieRepository(); // Se conecta a la clase que muestra accesa al archivo de peliculas
        public AdminReportForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Cuando el formulario abre, este muestra los reportes.
        /// </summary>
        private void AdminReportForm_Load(object sender, EventArgs e)
        {
            LoadReports(); // Llama a la función para cargar datos
        }
        /// <summary>
        /// Carga los reportes en la tabla, mostrando un promedio de calificación segun el genero de las peliculas .
        /// Muestra el estado de visualizacion y las películas recomendadas se encuentran recomendadas
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
                    .GroupBy(m => m.Genre) // Agrupa por género
                    .Select(g => new
                    {
                        Género = g.Key,
                        Cantidad = g.Count(),
                        Calificación_Promedio = g.Average(m => m.Rating).ToString("0.0"), // Calcula el promedio
                        Vistas = g.Count(m => m.ViewStatus == "Visto"), // Cantidad de películas vistas
                        Parcialmente_Vistas = g.Count(m => m.ViewStatus == "Parcialmente Visto"), // Cantidad de películas parcialmente vistas
                        No_Vistas = g.Count(m => m.ViewStatus == "No Vista"), //  Cantidad de películas no vistas
                        Recomendadas = g.Count(m => m.IsRecommended) // Cantidad de películas recomendadas
                    })
                    .OrderByDescending(g => g.Cantidad)
                    .ToList();
                dgvReports.DataSource = groupedMovies; // Se muestran los datos en la table de reportes
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los reportes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// se exportan los reportes a un archivo CSV.
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReports.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSV (*.csv)|*.csv";
                    saveFileDialog.FileName = "Reporte_Peliculas.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            // Genera encabezados
                            writer.WriteLine("Género,Cantidad,Calificación Promedio,Vistas,Parcialmente Vistas,No Vistas,Recomendadas");

                            // Escribe cada fila del reporte
                            foreach (var row in dgvReports.Rows.Cast<DataGridViewRow>())
                            {
                                var values = row.Cells.Cast<DataGridViewCell>()
                                    .Select(cell => cell.Value?.ToString() ?? "")
                                    .ToArray();
                                writer.WriteLine(string.Join(",", values));
                            }
                        }

                        MessageBox.Show("Reporte exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cierra la ventana de reportes y regresa al login.
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}