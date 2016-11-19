using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Services
{
    public class CookieService
    {
        private SecurityService security;
        public CookieService(SecurityService _security)
        {
            security = _security;
        }


    }
}
