using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1_TV_Track.Models
{
    /// <summary>
    /// Representa una película en el catálogo.
    /// Ahora incluye una calificación (1-5 estrellas), estado de vista y recomendación.
    /// </summary>
    public class Movie
    {
        public string Title { get; }
        public string Genre { get; }
        public string Platform { get; }
        public double Rating { get; private set; }  // ⭐ Nueva propiedad de calificación
        public string ViewStatus { get; private set; } // 📌 Estado de vista (Vista, Parcialmente Vista, No Vista)
        public bool IsRecommended { get; private set; } // 🔥 Nueva propiedad para recomendaciones

        /// <summary>
        /// Constructor de la clase Movie.
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
            Rating = rating;  // 📌 Guarda la calificación
            ViewStatus = viewStatus; // 📌 Guarda el estado de vista
            IsRecommended = isRecommended; // 📌 Indica si la película es recomendada
            UpdateRecommendation(); // 📌 Evalúa si debe recomendarse
        }

        /// <summary>
        /// Actualiza la calificación de la película y recalcula la recomendación.
        /// </summary>
        /// <param name="newRating">Nueva calificación (entre 1 y 5)</param>
        public void UpdateRating(double newRating)
        {
            if (newRating >= 1 && newRating <= 5)
            {
                Rating = newRating;  // 📌 Actualiza la calificación
                UpdateRecommendation(); // 📌 Recalcula si debe recomendarse
            }
        }

        /// <summary>
        /// Actualiza el estado de vista de la película y recalcula la recomendación.
        /// </summary>
        /// <param name="newStatus">Nuevo estado de vista ("Vista", "Parcialmente Vista", "No Vista")</param>
        public void UpdateViewStatus(string newStatus)
        {
            if (newStatus == "Vista" || newStatus == "Parcialmente Vista" || newStatus == "No Vista")
            {
                ViewStatus = newStatus;  // 📌 Actualiza el estado de vista
                UpdateRecommendation(); // 📌 Recalcula si debe recomendarse
            }
        }

        /// <summary>
        /// Evalúa si la película debe ser recomendada.
        /// Se recomienda si tiene 4 estrellas o más y no ha sido completamente vista.
        /// </summary>
        private void UpdateRecommendation()
        {
            IsRecommended = (Rating >= 4) && (ViewStatus == "No Vista" || ViewStatus == "Parcialmente Vista");
        }
    }
}
