using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Models
{
    public class Hall
    {
        public int Id { get; set; }
        public int HallNumber { get; set; }
        public int SeatNumbers { get; set; }
        public List<Show> Shows { get; set; }
    }
}
