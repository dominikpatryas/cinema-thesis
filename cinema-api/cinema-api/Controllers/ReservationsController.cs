using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using cinema_api.Data.Interfaces;
using cinema_api.Dtos.Reservations;
using cinema_api.Helpers.Interfaces;
using cinema_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinema_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationsController : Controller
    {
        private IReservationsRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAuthorizer _authorizer;

        public ReservationsController(IReservationsRepository repo, IMapper mapper, IAuthorizer authorizer)
        {
            _repo = repo;
            _mapper = mapper;
            _authorizer = authorizer;
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(ReservationForAddDto reservationForAdd)
        {
            var reservation = _mapper.Map<Reservation>(reservationForAdd);

            foreach (var seat in reservationForAdd.SeatsReserved)
            {
                seat.ShowId = reservationForAdd.ShowId;
            }

            _repo.AddReservation(reservation);
            await _repo.SaveAll();

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _repo.GetReservations();

            return StatusCode(200, reservations);
        }

        [Authorize]
        [HttpGet("management")]
        public async Task<IActionResult> GetManagementReservations()
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            var reservations = await _repo.GetManagementReservations();

            return StatusCode(200, reservations);
        }

        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetReservation(int reservationId)
        {
            var reservation = await _repo.GetReservation(reservationId);

            return StatusCode(200, reservation);
        }

        [Authorize]
        [HttpPatch("{reservationId}")]
        public async Task<IActionResult> ConfirmReservation(int reservationId)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            await _repo.ConfirmReservation(reservationId);

            return StatusCode(200);
        }

        [Authorize]
        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            await _repo.DeleteReservation(reservationId);

            return StatusCode(200);
        }
    }
}
