using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Dtos
{
    public class MoviesForListsDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TrailerUrl { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
