using cinema_api.Data.Interfaces;
using cinema_api.Dtos;
using cinema_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data
{
    public class HallsRepository : IHallsRepository
    {
        private readonly DataContext _context;
        public HallsRepository(DataContext context)
        {
            _context = context;
        }

        public void AddHall(Hall hall)
        {
            _context.Halls.Add(hall);
        }

        public async Task DeleteHall(int id)
        {
            var hall = _context.Halls.FirstOrDefault(h => h.Id == id);

            _context.Remove(hall);
            await SaveAll();
        }

        public async Task<Hall> GetHall(int id)
        {
            var hall = await _context.Halls
                .Include(h => h.Shows)
                .FirstOrDefaultAsync(h2 => h2.Id == id);

            return hall;
        }

        public async Task<IEnumerable<Hall>> GetHalls()
        {
            var halls = await _context.Halls.Include(h => h.Shows).ToListAsync();

            return halls;
        }

        public bool IsHallExisting()
        {
            throw new System.NotImplementedException();
        }

        public bool CheckAvailability(int id, DateTime dateTime, int movieDuration)
        {
            var shows = _context.Shows.Include(m => m.Movie).Where(x => x.HallId == id).ToList();
            var dateTimeEndOfShow = dateTime.AddMinutes(movieDuration);

            foreach (var show in shows)
            {
                if ((show.DatePlayed <= dateTime) && (dateTime <= show.DatePlayed.AddMinutes(show.Movie.Duration) 
                    || (show.DatePlayed <= dateTimeEndOfShow) && (dateTimeEndOfShow <= show.DatePlayed.AddMinutes(show.Movie.Duration))
                    || (show.DatePlayed >= dateTime && show.DatePlayed.AddMinutes(show.Movie.Duration) <= dateTimeEndOfShow)))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
