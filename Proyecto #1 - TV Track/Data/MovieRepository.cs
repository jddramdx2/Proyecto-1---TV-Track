﻿using System;
using System.Collections.Generic;
using System.IO;
using Proyecto_1_TV_Track.Models;

namespace Proyecto_1_TV_Track.Data
{
    /// <summary>
    /// Maneja el acceso al archivo CSV de películas.
    /// Ahora también soporta calificaciones, estado de vista y recomendaciones.
    /// </summary>
    public class MovieRepository
    {
        private readonly string filePath = "listado_100_peliculas.csv"; // Ruta del CSV

        /// <summary>
        /// Obtiene la lista de películas desde el archivo CSV.
        /// Ahora incluye la calificación, estado de vista y si es recomendada.
        /// </summary>
        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                if (!File.Exists(filePath))
                {
                    return movies; // 📌 Si el archivo no existe, regresa una lista vacía.
                }

                string[] lines = File.ReadAllLines(filePath);
                for (int i = 1; i < lines.Length; i++) // 📌 Se omite el encabezado.
                {
                    string[] data = lines[i].Split(',');

                    // 📌 Verificar si hay al menos 6 columnas para incluir la recomendación.
                    double rating = (data.Length >= 4 && double.TryParse(data[3], out double parsedRating)) ? parsedRating : 0;
                    string viewStatus = (data.Length >= 5) ? data[4].Trim() : "No Vista"; // 📌 Si no hay estado, se asume "No Vista".
                    bool isRecommended = (data.Length >= 6 && bool.TryParse(data[5], out bool parsedRecommendation)) ? parsedRecommendation : CalculateRecommendation(rating, viewStatus);

                    movies.Add(new Movie(data[0].Trim(), data[1].Trim(), data[2].Trim(), rating, viewStatus, isRecommended));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al leer las películas: {ex.Message}");
            }
            return movies;
        }

        /// <summary>
        /// Agrega una calificación a una película o actualiza la existente.
        /// También recalcula si la película debe ser recomendada.
        /// </summary>
        public void RateMovie(string title, double newRating)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("⚠ El archivo de películas no existe.");
                    return;
                }

                var lines = File.ReadAllLines(filePath);
                bool updated = false;

                for (int i = 1; i < lines.Length; i++) // 📌 Se omite el encabezado.
                {
                    string[] data = lines[i].Split(',');

                    if (data[0].Trim().Equals(title, StringComparison.OrdinalIgnoreCase))
                    {
                        if (data.Length < 6)
                        {
                            Array.Resize(ref data, 6); // 📌 Asegura que haya espacio para recomendación.
                        }
                        data[3] = newRating.ToString("0.0"); // 📌 Actualiza la calificación.

                        // 📌 Recalcula si debe recomendarse
                        data[5] = CalculateRecommendation(newRating, data[4]).ToString();

                        lines[i] = string.Join(",", data);
                        updated = true;
                        break;
                    }
                }

                if (updated)
                {
                    File.WriteAllLines(filePath, lines); // 📌 Guarda cambios en el CSV.
                    Console.WriteLine($"✅ Calificación y recomendación actualizada para '{title}'.");
                }
                else
                {
                    Console.WriteLine($"⚠ No se encontró la película '{title}' en el archivo.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al actualizar la calificación: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza el estado de vista de una película en el archivo CSV.
        /// También recalcula si la película debe ser recomendada.
        /// </summary>
        public void UpdateMovieViewStatus(string title, string viewStatus)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("⚠ El archivo de películas no existe.");
                    return;
                }

                var lines = File.ReadAllLines(filePath);
                bool updated = false;

                for (int i = 1; i < lines.Length; i++) // 📌 Se omite el encabezado.
                {
                    string[] data = lines[i].Split(',');

                    if (data[0].Trim().Equals(title, StringComparison.OrdinalIgnoreCase))
                    {
                        if (data.Length < 6)
                        {
                            Array.Resize(ref data, 6); // 📌 Asegura que haya espacio para recomendación.
                        }
                        data[4] = viewStatus; // 📌 Actualiza el estado de vista.

                        // 📌 Recalcula si debe recomendarse
                        double rating = double.TryParse(data[3], out double parsedRating) ? parsedRating : 0;
                        data[5] = CalculateRecommendation(rating, viewStatus).ToString();

                        lines[i] = string.Join(",", data);
                        updated = true;
                        break;
                    }
                }

                if (updated)
                {
                    File.WriteAllLines(filePath, lines); // 📌 Guarda cambios en el CSV.
                    Console.WriteLine($"✅ Estado de vista y recomendación actualizados para '{title}'.");
                }
                else
                {
                    Console.WriteLine($"⚠ No se encontró la película '{title}' en el archivo.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al actualizar el estado de vista: {ex.Message}");
            }
        }

        /// <summary>
        /// Determina si una película debe ser recomendada en base a su calificación y estado de vista.
        /// </summary>
        private bool CalculateRecommendation(double rating, string viewStatus)
        {
            return rating >= 4.0 && viewStatus != "No Vista"; // 📌 Se recomienda si tiene >= 4 estrellas y no está en "No Vista".
        }
    }
}