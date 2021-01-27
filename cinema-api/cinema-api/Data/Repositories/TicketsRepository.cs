using cinema_api.Data.Interfaces;
using cinema_api.Models.HelperModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data.Repositories
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly DataContext _context;
        public TicketsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> AddTicket(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await SaveAll();

            return ticket.Id;
        }

        public void DeleteTicket(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(h => h.Id == id);

            _context.Remove(ticket);
        }

        public async Task<Ticket> GetTicket(int id)
        {
            var ticket = await _context.Tickets
                .Include(x => x.SeatsReserved)
                .Include(z => z.Show.Hall)
                .Include(z => z.Show.Movie)
                .Include(z => z.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            return ticket;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
