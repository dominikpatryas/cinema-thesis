using cinema_api.Dtos;
using cinema_api.Models;
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

        public Task<Movie> GetMovie(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Movie> GetMovies()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsMovieExisting(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
