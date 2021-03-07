using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Dtos
{
    public class MoviesForListsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TrailerUrl { get; set; }
        public int Year { get; set; }
        public ICollection<MovieType> Types { get; set; }
        public int Duration { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
