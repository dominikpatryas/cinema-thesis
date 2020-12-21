using cinema_api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Models.HelperModels
{
    public class Ticket
    {
        public int Id { get; set; }
        public TicketType TicketType { get; set; }
        public List<SeatReserved> SeatsReserved { get; set; }
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public Show Show { get; set; }
        public int ReservationId { get; set; }
    }
}


