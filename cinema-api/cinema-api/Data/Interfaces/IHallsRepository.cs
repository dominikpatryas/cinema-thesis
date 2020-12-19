using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data.Interfaces
{
    public interface IHallsRepository
    {
        Task<IEnumerable<Hall>> GetHalls();
        Task<Show> GetHall(int id);
        void AddHall(Hall hall);
        Task<Boolean> DeleteHall(int id);
        Boolean IsHallExisting();
        Task<Boolean> SaveAll();
    }
}
