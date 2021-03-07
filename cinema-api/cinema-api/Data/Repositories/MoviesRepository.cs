using cinema_api.Dtos;
using cinema_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly DataContext _context;
        public MoviesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Movie> GetMovie(int id)
        {
            var movie = await _context.Movies
                .Include(x => x.Photos)
                .Include(x => x.Casts)
                .Include(x => x.Types)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movies = await _context.Movies
                .Include(x => x.Photos)
                .Include(x => x.Casts)
                .Include(x => x.Types)
                .ToListAsync();
            
            return movies;
        }

        public bool IsMovieExisting(string name)
        {
             return _context.Movies.Any(x => x.Title == name);
        }

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
        }

        public async Task DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            _context.Remove(movie);
            await SaveAll();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
