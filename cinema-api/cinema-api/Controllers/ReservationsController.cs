using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using cinema_api.Data.Interfaces;
using cinema_api.Dtos.Reservations;
using cinema_api.Dtos.Users;
using cinema_api.Helpers.Interfaces;
using cinema_api.Models;
using cinema_api.Models.HelperModels;
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
        private readonly ITicketsRepository _ticketsRepository;

        public ReservationsController(IReservationsRepository repo, IMapper mapper, IAuthorizer authorizer, ITicketsRepository ticketsRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _authorizer = authorizer;
            _ticketsRepository = ticketsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(ReservationForAddDto reservationForAdd)
        {
            var reservation = _mapper.Map<Reservation>(reservationForAdd);

            foreach (var seat in reservationForAdd.SeatsReserved)
            {
                seat.ShowId = reservationForAdd.ShowId;
            }

            var reservationId = await _repo.AddReservation(reservation);
            var ticketId = await _ticketsRepository.AddTicket(new Ticket
            {
                Normal = reservationForAdd.NormalTickets,
                Reduced = reservationForAdd.ReducedTickets,
                ReservationId = reservationId,
                SeatsReserved = reservationForAdd.SeatsReserved,
                ShowId = reservationForAdd.ShowId,
                UserId = reservationForAdd.UserId
            });
            await _repo.UpdateTicketId(ticketId, reservationId);
            await _repo.SaveAll();

            return StatusCode(201, reservationId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _repo.GetReservations();

            var reservationsDto = _mapper.Map<List<ReservationsForUserReservationsListDto>>(reservations);

            return StatusCode(200, reservationsDto);
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
