using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cinema_api.Data;
using cinema_api.Dtos;
using cinema_api.Models;
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
        
        public MoviesController(IMoviesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMovie(MovieForAddDto movieForAdd)
        {
            foreach (var entry in movieForAdd.Photos)
            {
                entry.MovieTitle = movieForAdd.Title;
            }

            var movieForAddModel = _mapper.Map<Movie>(movieForAdd);

            _repo.AddMovie(movieForAddModel);
            await _repo.SaveAll();

            return StatusCode(201);
        }

        [HttpGet("")]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieForUpdateDto movieForUpdate)
        {
            var movieFromrepo = await _repo.GetMovie(id);

            _mapper.Map(movieForUpdate, movieFromrepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpGet("exists/{title}")]
        public async Task<IActionResult> IsMovieExisting(string title)
        {
            var isExisting = _repo.IsMovieExisting(title);

            return StatusCode(200, isExisting);
        }

    }

}
