using cinema_api.Dtos.Casts;
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
        public ICollection<PhotoForAddDto> Photos { get; set; }
        public ICollection<CastForAddDto> Casts { get; set; }
    }
}
