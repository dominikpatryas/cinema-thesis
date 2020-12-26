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
        Task<IEnumerable<Reservation>> GetManagementReservations();
        Task<Reservation> GetReservation(int id);
        Task<Boolean> ConfirmReservation(int id);
        void AddReservation(Reservation reservation);
        Task DeleteReservation(int id);
        Task<Boolean> SaveAll();
    }
}
