using cinema_api.Dtos.Users;
using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Dtos
{
    public class TicketForGenerateDto
    {
        public int Normal { get; set; }
        public int Reduced { get; set; }
        public List<SeatReserved> SeatsReserved { get; set; }
        public UserForTicketDto User { get; set; }
        public int ShowId { get; set; }
        public Show Show { get; set; }
        public int ReservationId { get; set; }
    }
}
