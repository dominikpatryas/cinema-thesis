using cinema_api.Data.Interfaces;
using cinema_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data.Repositories
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly DataContext _context;
        public ReservationsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<int> AddReservation(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);

            _context.Reservations.Remove(reservation);

            await SaveAll();
        }

        public async Task<Reservation> GetReservation(int id)
        {
            var reservation = await _context.Reservations.Include(x => x.SeatsReserved).FirstOrDefaultAsync(x => x.Id == id);
            
            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var reservations = await _context.Reservations
                .Include(x => x.SeatsReserved)
                .Include(x => x.Show)
                .Include(x => x.User)
                .ToListAsync();

            return reservations;
        }

        public async Task<IEnumerable<Reservation>> GetManagementReservations()
        {
            var reservations = await _context.Reservations
                .Include(x => x.SeatsReserved)
                .Where(y => y.IsConfirmed == false)
                .ToListAsync();

            return reservations;
        }

        public async Task<Boolean> ConfirmReservation(int id)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);

            reservation.IsConfirmed = true;
            await SaveAll();

            return true;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task RemoveTemporaryUser(int reservationUserId)
        {
            var temporaryUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == reservationUserId && u.TemporaryReservation == true);

            if (temporaryUser != null)
            {
                _context.Users.Remove(temporaryUser);
            }
        }

        public async Task UpdateTicketId(int ticketId, int reservationId)
        {
            var reservationToUpdate = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
            reservationToUpdate.TicketId = ticketId;

            await SaveAll();
        }
    }
}
