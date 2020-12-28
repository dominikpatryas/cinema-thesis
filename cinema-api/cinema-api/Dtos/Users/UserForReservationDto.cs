using cinema_api.Models;
using cinema_api.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Dtos.Users
{
    public class UserForReservationDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ReservationForReservationDto> Reservations { get; set; }
        public List<Ticket> Ticket { get; set; }
    }
}
