using cinema_api.Enums;
using cinema_api.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public List<Seat> SeatsReserved { get; set; }
        public int isConfirmed { get; set; }
    }
}
