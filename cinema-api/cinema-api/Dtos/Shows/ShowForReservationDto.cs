using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Dtos.Shows
{
    public class ShowForReservationDto
    {
        public int Id { get; set; }
        public DateTime DatePlayed { get; set; }
        public Hall Hall { get; set; }
        public Movie Movie { get; set; }
    }
}
