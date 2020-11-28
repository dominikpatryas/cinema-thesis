using cinema_api.Models;
using System;
using System.Collections.Generic;

namespace cinema_api.Dtos
{
    public class MovieForAddDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TrailerUrl { get; set; }
        public DateTime DatePlayed { get; set; }
        public ICollection<PhotoForAdd> Photos { get; set; }
    }
}
