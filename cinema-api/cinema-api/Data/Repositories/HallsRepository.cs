using cinema_api.Data.Interfaces;
using cinema_api.Dtos;
using cinema_api.Models;
using Microsoft.EntityFrameworkCore;
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

        public Task<bool> DeleteHall(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Show> GetHall(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Hall>> GetHalls()
        {
            throw new System.NotImplementedException();
        }

        public bool IsHallExisting()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
