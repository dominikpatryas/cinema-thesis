using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cinema_api.Helpers.Interfaces
{
    public interface IAuthorizer
    {
        public bool IsAdmin(ClaimsPrincipal _user);
        public bool IsEmployee(ClaimsPrincipal _user);
        public bool IsAdminOrEmployee(ClaimsPrincipal _user);
    }
}
