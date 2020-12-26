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
        Task<Hall> GetHall(int id);
        void AddHall(Hall hall);
        Task DeleteHall(int id);
        Boolean IsHallExisting();
        Task<Boolean> SaveAll();
    }
}
