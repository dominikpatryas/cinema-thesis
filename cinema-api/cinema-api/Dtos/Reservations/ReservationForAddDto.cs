using cinema_api.Models;
using cinema_api.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Dtos.Reservations
{
    public class ReservationForAddDto
    {
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public List<SeatReserved> SeatsReserved { get; set; }
        public int isConfirmed { get; set; }
    }
}
