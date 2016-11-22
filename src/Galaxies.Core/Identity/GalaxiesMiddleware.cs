using Galaxies.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Identity
{
    public class GalaxiesMiddleware
    {
        private readonly ILogger<GalaxiesMiddleware> _logger;
        private readonly RequestDelegate _next;

        public GalaxiesMiddleware(RequestDelegate next, ILogger<GalaxiesMiddleware> logger, GalaxiesMaker maker = null)
        {
            if (maker == null)
                throw new ArgumentNullException("add galaxies service before use it");
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, UserService userService, CookieService cookieService)
        {
            var user = cookieService.GetUser();
            if (null != user)
            {

            }
        }
    }
}
