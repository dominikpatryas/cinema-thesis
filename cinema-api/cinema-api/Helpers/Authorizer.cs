using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using cinema_api.Data;
using cinema_api.Dtos;
using cinema_api.Helpers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cinema_api.Helpers
{
    public class Authorizer : IAuthorizer
    {
        public bool IsAdmin(ClaimsPrincipal _user)
        {
            if (_user.FindFirst("Admin").Value == "true")
                return true;

            return false;
        }

        public bool IsEmployee(ClaimsPrincipal _user)
        {
            if (_user.FindFirst("Employee").Value == "true")
                return true;

            return false;
        }

        public bool IsAdminOrEmployee(ClaimsPrincipal _user)
        {
            if (bool.Parse(_user.FindFirst("Employee").Value) || bool.Parse(_user.FindFirst("Admin").Value) == true)
                return true;

            return false;
        }
    }
}
