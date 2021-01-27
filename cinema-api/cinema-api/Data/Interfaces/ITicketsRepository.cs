using cinema_api.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data.Interfaces
{
    public interface ITicketsRepository
    {
        Task<Ticket> GetTicket(int id);
        Task<int> AddTicket(Ticket ticket);
        void DeleteTicket(int id);
        Task<Boolean> SaveAll();

    }
}
