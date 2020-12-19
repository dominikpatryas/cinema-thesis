using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data.Interfaces
{
    public interface IReservationsRepository
    {
        Task<IEnumerable<Reservation>> GetReservations();
        Task<Reservation> GetReservation(int id);
        void AddReservation(Reservation reservation);
        Task<Boolean> DeleteReservation(int id);
        Task<Boolean> SaveAll();
    }
}
