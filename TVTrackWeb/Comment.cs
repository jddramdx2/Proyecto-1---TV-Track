using System;

namespace TVTrackWeb
{
    public class Comment
    {
        public string Text { get; set; }
        public DateTime Fecha { get; set; }
        public Comment (string text)
        {  Text = text;
            Fecha = DateTime.Now;
        }
    }
}
