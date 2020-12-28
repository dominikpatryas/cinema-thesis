using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Models
{
    public class Show
    {
        public int Id { get; set; }
        public DateTime DatePlayed { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public List<SeatReserved> SeatsReserved { get; set; }
    }
}