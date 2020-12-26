using cinema_api.Data.Interfaces;
using cinema_api.Dtos;
using cinema_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data
{
    public class ShowsRepository : IShowsRepository
    {
        private readonly DataContext _context;
        public ShowsRepository(DataContext context)
        {
            _context = context;
        }

        public void AddShow(Show show)
        {
            _context.Shows.Add(show);
        }

        public async Task DeleteShow(int id)
        {
            var show = _context.Shows
                .Include(r => r.SeatsReserved)
                .FirstOrDefault(s => s.Id == id);

            var reservations = _context.Reservations
                .Include(z=>z.SeatsReserved)
                .Where(r => r.ShowId == id);

            _context.Remove(show);
            _context.RemoveRange(reservations);
            await SaveAll();
        }

        public async Task<IEnumerable<Show>> GetShows()
        {
            var shows = await _context.Shows
                .Include(x => x.SeatsReserved)
                .Include(z => z.Hall)
                .ToListAsync();

            return shows;
        }

        public async Task<IEnumerable<Show>> GetShowsForMovie(int id)
        {
            var shows = await _context.Shows
                .Include(x => x.SeatsReserved)
                .Include(z => z.Hall)
                .Where(y => y.MovieId == id)
                .ToListAsync();

            return shows;
        }

        public async Task<Show> GetShow(int id)
        {
            var show = await _context.Shows
                .Include(x => x.SeatsReserved)
                .Include(z => z.Hall)
                .FirstOrDefaultAsync(x => x.Id == id);

            return show;
        }

        public bool IsShowExisting()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
