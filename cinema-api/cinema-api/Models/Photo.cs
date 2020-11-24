using System;

namespace cinema_api.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string MovieTitle { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
