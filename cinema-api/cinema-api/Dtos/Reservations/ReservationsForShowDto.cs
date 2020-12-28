using cinema_api.Models;
using cinema_api.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Dtos.Reservations
{
    public class ReservationsForShow
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ReservationUserId { get; set; }
        public int ShowId { get; set; }
        public Show Show { get; set; }
        public List<SeatReserved> SeatsReserved { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
