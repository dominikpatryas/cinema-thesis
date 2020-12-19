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
        public List<Seat> Seats { get; set; }
        public int UserId { get; set; }
    }
}


