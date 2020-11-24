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
        public async Task<IActionResult> AddMovie(MovieForAdd movieForAdd)
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
    }
}
