using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Models
{
    public class SeatReserved
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public int ShowId { get; set; }
    }
}
