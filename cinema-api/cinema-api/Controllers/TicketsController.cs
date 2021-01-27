using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cinema_api.Data.Interfaces;
using cinema_api.Dtos;
using cinema_api.Helpers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinema_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : Controller
    {
        private ITicketsRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAuthorizer _authorizer;

        public TicketsController(ITicketsRepository repo, IMapper mapper, IAuthorizer authorizer)
        {
            _repo = repo;
            _mapper = mapper;
            _authorizer = authorizer;
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            if (!_authorizer.IsAdminOrEmployee(User))
                return Unauthorized();

            var ticket = await _repo.GetTicket(id);
            var ticketForReservation = _mapper.Map<TicketForGenerateDto>(ticket);

            return StatusCode(200, ticketForReservation);
        }
    }
}
