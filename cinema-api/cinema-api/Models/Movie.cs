using System;
using System.Collections.Generic;

namespace cinema_api.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TrailerUrl { get; set; }
        public ICollection<Cast> Casts { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
