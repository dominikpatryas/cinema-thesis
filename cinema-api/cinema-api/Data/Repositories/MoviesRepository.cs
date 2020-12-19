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

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
        }

        public Task<bool> DeleteMovie(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Movie> GetMovie(int id)
        {
            var movie = await _context.Movies.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movies = await _context.Movies.Include(x => x.Photos).ToListAsync();
            
            return movies;
        }

        public bool IsMovieExisting(string name)
        {
             return _context.Movies.Any(x => x.Title == name);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
