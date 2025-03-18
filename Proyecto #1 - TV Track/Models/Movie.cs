using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1_TV_Track.Models
{
    /// <summary>
    /// Represents a movie in the system.
    /// </summary>
    public class Movie
    {
        public string Title { get; }
        public string Genre { get; }
        public string Platform { get; }

        public Movie(string title, string genre, string platform)
        {
            Title = title;
            Genre = genre;
            Platform = platform;
        }
    }
}
