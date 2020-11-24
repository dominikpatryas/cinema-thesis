using cinema_api.Dtos;
using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data
{
    public interface IMoviesRepository
    {
        Task<Movie> GetMovies();
        Task<Movie> GetMovie(int id);
        void AddMovie(Movie movie);
        Task<Boolean> DeleteMovie(int id);
        Task<Boolean> IsMovieExisting(string name);
        Task<Boolean> SaveAll();
    }
}
