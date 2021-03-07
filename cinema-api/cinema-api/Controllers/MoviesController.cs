using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cinema_api.Data;
using cinema_api.Dtos;
using cinema_api.Helpers;
using cinema_api.Helpers.Interfaces;
using cinema_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cinema_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private IMoviesRepository _repo;
        private readonly IMapper _mapper;
        private IAuthorizer _authorizer;

        public MoviesController(IMoviesRepository repo, IMapper mapper, IAuthorizer authorizer)
        {
            _repo = repo;
            _mapper = mapper;
            _authorizer = authorizer;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieForAddDto movieForAdd)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            foreach (var entry in movieForAdd.Photos)
            {
                entry.MovieTitle = movieForAdd.Title;
            }

            var movieForAddModel = _mapper.Map<Movie>(movieForAdd);

            _repo.AddMovie(movieForAddModel);
            await _repo.SaveAll();

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _repo.GetMovies();

            return StatusCode(200, movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _repo.GetMovie(id);

            return StatusCode(200, movie);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieForUpdateDto movieForUpdate)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            var movieFromRepository = await _repo.GetMovie(id);

            _mapper.Map(movieForUpdate, movieFromRepository);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        [Authorize]
        [HttpGet("exists/{title}")]
        public async Task<IActionResult> IsMovieExisting(string title)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            var isExisting = _repo.IsMovieExisting(title);

            return StatusCode(200, isExisting);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            await _repo.DeleteMovie(id);

            return StatusCode(200);
        }

    }

}
