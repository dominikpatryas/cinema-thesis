using cinema_api.Dtos.Shows;
using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Dtos.Users
{
    public class ReservationForReservationDto
    {
        public int Id { get; set; }
        public ShowForReservationDto Show { get; set; }
        public List<SeatReserved> SeatsReserved { get; set; }
        public bool IsConfirmed { get; set; }
        public int TicketId { get; set; }
    }
}
