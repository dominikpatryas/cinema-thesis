using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Dtos.Shows
{
    public class ShowForAddDto
    {
        public DateTime DatePlayed { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }
    }
}
