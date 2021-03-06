﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cinema_api.Data.Interfaces;
using cinema_api.Dtos.Halls;
using cinema_api.Dtos.Shows;
using cinema_api.Helpers;
using cinema_api.Helpers.Interfaces;
using cinema_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinema_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HallsController : Controller
    {
        private IHallsRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAuthorizer _authorizer;

        public HallsController(IHallsRepository repo, IMapper mapper, IAuthorizer authorizer)
        {
            _repo = repo;
            _mapper = mapper;
            _authorizer = authorizer;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddHall(HallForAddDto hallForAdd)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            var hall = _mapper.Map<Hall>(hallForAdd);

            _repo.AddHall(hall);
            await _repo.SaveAll();

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetHalls()
        {
            var halls = await _repo.GetHalls();

            return StatusCode(200, halls);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            await _repo.DeleteHall(id);

            return StatusCode(200);
        }

        [HttpPost("availability")]
        [Authorize]
        public async Task<IActionResult> CheckAvailability(HallAvailability hallAvailability)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            var result = _repo.CheckAvailability(hallAvailability.Id, hallAvailability.DateTime, hallAvailability.MovieDuration);

            return StatusCode(200, result);
        }
    }
}
