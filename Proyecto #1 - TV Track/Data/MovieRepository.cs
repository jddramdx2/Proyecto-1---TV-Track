using System;
using System.Collections.Generic;
using System.IO;
using Proyecto_1_TV_Track.Models;

namespace Proyecto_1_TV_Track.Data
{
    /// <summary>
    /// Acceso al archivo de pelis 
    /// </summary>
    public class MovieRepository
    {
        private string filePath = "listado_100_peliculas.csv"; // ruta csv

        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();

            try
            {
                if (!File.Exists(filePath))
                {
                    return movies;
                }

                string[] lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++) // Ignorar encabezados
                {
                    string[] data = lines[i].Split(',');

                    if (data.Length == 3)
                    {
                        movies.Add(new Movie(data[0], data[1], data[2]));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading movies: {ex.Message}");
            }

            return movies;
        }
    }
}
