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
        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
        }

        public Task<bool> DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Reservation> GetReservation(int id)
        {
            var reservation = await _context.Reservations.Include(x => x.SeatsReserved).FirstOrDefaultAsync(x => x.Id == id);
            
            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var reservations = await _context.Reservations.Include(x => x.SeatsReserved).ToListAsync();

            return reservations;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
