using AutoMapper;
using cinema_api.Dtos;
using cinema_api.Dtos.Casts;
using cinema_api.Dtos.Halls;
using cinema_api.Dtos.Reservations;
using cinema_api.Dtos.Shows;
using cinema_api.Dtos.Users;
using cinema_api.Models;
using cinema_api.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Helpers
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserForReservationDto>();
            CreateMap<Ticket, TicketForGenerateDto>();
            CreateMap<MovieForAddDto, Movie>();
            CreateMap<ShowForAddDto, Show>();
            CreateMap<PhotoForAddDto, Photo>();
            CreateMap<HallForAddDto, Hall>();
            CreateMap<CastForAddDto, Cast>();
            CreateMap<ReservationForAddDto, Reservation>();
            CreateMap<ShowForReservationDto, Show>();
            CreateMap<UserForReservationDto, ReservationForReservationDto>();
            CreateMap<MovieForUpdateDto, Movie>();
        }
    }
}
