using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cinema_api.Data.Interfaces;
using cinema_api.Dtos.Shows;
using cinema_api.Helpers.Interfaces;
using cinema_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinema_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShowsController : Controller
    {
        private IShowsRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAuthorizer _authorizer;

        public ShowsController(IShowsRepository repo, IMapper mapper, IAuthorizer authorizer)
        {
            _repo = repo;
            _mapper = mapper;
            _authorizer = authorizer;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddShow(ShowForAddDto showForAdd)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            var showModel = _mapper.Map<Show>(showForAdd);

            _repo.AddShow(showModel);
            await _repo.SaveAll();

            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShow(int id)
        {
            var show = await _repo.GetShow(id);

            return StatusCode(200, show);
        }

        [HttpGet]
        public async Task<IActionResult> GetShows()
        {
            var shows = await _repo.GetShows();

            return StatusCode(200, shows);
        }

        [HttpGet("{id}/all")]
        public async Task<IActionResult> GetShows(int id)
        {
            var shows = await _repo.GetShowsForMovie(id);

            return StatusCode(200, shows);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            await _repo.DeleteShow(id);

            return StatusCode(200);
        }
    }
}
