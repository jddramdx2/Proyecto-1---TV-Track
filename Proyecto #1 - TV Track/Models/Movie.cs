using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1_TV_Track.Models
{
    /// <summary>
    /// Muestra la lista de peliculas del CSV en un catalogo.
    /// Se puede incluir la calificación (1-5 estrellas), si fue vista, parcialmente vista o no y agrega las recomendaciónes.
    /// </summary>
    public class Movie
    {
        public string Title { get; }
        public string Genre { get; }
        public string Platform { get; }
        public double Rating { get; private set; }  //  Propiedad de calificación
        public string ViewStatus { get; private set; } //  Estado (Vista, Parcialmente Vista, No Vista)
        public bool IsRecommended { get; private set; } // Propiedad para recomendaciones

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="title">Título de la película</param>
        /// <param name="genre">Género de la película</param>
        /// <param name="platform">Plataforma de streaming</param>
        /// <param name="rating">Calificación de la película (opcional, por defecto 0)</param>
        /// <param name="viewStatus">Estado de vista (opcional, por defecto "No Vista")</param>
        /// <param name="isRecommended">Indica si la película es recomendada (opcional, por defecto `false`)</param>
        public Movie(string title, string genre, string platform, double rating = 0, string viewStatus = "No Vista", bool isRecommended = false)
        {
            Title = title;
            Genre = genre;
            Platform = platform;
            Rating = rating;  //  Guarda calificación
            ViewStatus = viewStatus; //  Guarda estado de visualizacion
            IsRecommended = isRecommended; // Indica si la película es recomendada
            UpdateRecommendation(); // Evalúa si debe recomendarse
        }

        /// <summary>
        /// Actualización calificación de la película y recalcula la recomendación.
        /// </summary>
        /// <param name="newRating">Nueva calificación (entre 1 y 5)</param>
        public void UpdateRating(double newRating)
        {
            if (newRating >= 1 && newRating <= 5)
            {
                Rating = newRating;  // Actualiza calificación
                UpdateRecommendation(); // Recalcula las recomendaciónes
            }
        }

        /// <summary>
        /// Actualiza el estado de las película y recalcula la recomendaciónes.
        /// </summary>
        /// <param name="newStatus">Nuevo estado  ("Vista", "Parcialmente Vista", "No Vista")</param>
        public void UpdateViewStatus(string newStatus)
        {
            if (newStatus == "Vista" || newStatus == "Parcialmente Vista" || newStatus == "No Vista")
            {
                ViewStatus = newStatus;  // 📌 Actualiza el estado de visualizacion
                UpdateRecommendation(); // 📌 Recalcula si debe recomendarse
            }
        }

        /// <summary>
        /// Evalúa si la película puede ser recomendada.
        /// Se recomienda si tiene 4 o más estrellas y no ha sido completamente vista.
        /// </summary>
        private void UpdateRecommendation()
        {
            IsRecommended = (Rating >= 4) && (ViewStatus == "No Vista" || ViewStatus == "Parcialmente Vista");
        }
    }
}
