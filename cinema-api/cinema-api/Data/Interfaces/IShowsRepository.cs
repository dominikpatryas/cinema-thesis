using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data.Interfaces
{
    public interface IShowsRepository
    {
        Task<IEnumerable<Show>> GetShows();
        Task<IEnumerable<Show>> GetShowsForMovie(int id);
        Task<Show> GetShow(int id);
        void AddShow(Show show);
        Task DeleteShow(int id);
        Boolean IsShowExisting();
        Task<Boolean> SaveAll();
    }
}
