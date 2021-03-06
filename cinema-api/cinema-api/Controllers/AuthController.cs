﻿using AutoMapper;
using cinema_api.Data;
using cinema_api.Dtos;
using cinema_api.Dtos.Users;
using cinema_api.Helpers.Interfaces;
using cinema_api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace cinema_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IAuthorizer _authorizer;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper, IAuthorizer authorizer)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
            _authorizer = authorizer;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Email.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Email, userFromRepo.Email.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.FirstName.ToString()),
                new Claim("Admin", userFromRepo.Admin.ToString(), ClaimValueTypes.Boolean),
                new Claim("Employee", userFromRepo.Employee.ToString(), ClaimValueTypes.Boolean)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(365),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                userId = userFromRepo.Id
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Email = userForRegisterDto.Email.ToLower();

            if (await _repo.UserExists(userForRegisterDto.Email))
                return BadRequest("Email exists");

            var userToCreate = _mapper.Map<User>(userForRegisterDto);
            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            if (userForRegisterDto.TemporaryReservation)
            {
                return StatusCode(201, createdUser.Id);
            } 
            else
            {
                return StatusCode(201);
            }
        }

        [Authorize]
        [HttpGet("reservations")]
        public async Task<IActionResult> GetUserReservations()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = _authorizer.GetUserClaim(token, "nameid");
            
            var user = await _repo.GetUser(Int32.Parse(userId));
            var userReservations = _mapper.Map<UserForReservationDto>(user);
            
            return StatusCode(200, userReservations);
        }
    }
}
