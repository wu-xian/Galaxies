using Galaxies.Model.EntityModel;
using Galaxies.Model.LogicModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Services
{
    public class CookieService
    {
        private readonly SecurityService _security;
        private readonly UserService _userService;
        private readonly HttpContext _httpContext;
        private readonly ILogger<CookieService> _logger;
        private static readonly string COOKIE_NAME = "GALAXIES";
        private static readonly string COOKIE_DOMAIN = "GALAXIES_CORE";
        private static readonly string COOKIE_PATH = "/";
        public CookieService(SecurityService security, UserService userService, HttpContext httpContext, ILogger<CookieService> logger)
        {
            _security = security;
            _userService = userService;
            _httpContext = httpContext;
            _logger = logger;
        }

        public UserStore GetUser()
        {
            var cookieSecurityString = _httpContext.Request.Cookies[COOKIE_NAME];
            if (string.IsNullOrEmpty(cookieSecurityString))
            {
                return null;
            }
            var cookieDecryptionString = _security.Decryption(cookieSecurityString);
            UserStore userStore;
            try
            {
                userStore = JsonConvert.DeserializeObject<UserStore>(cookieSecurityString);
            }
            catch (Exception e)
            {
                _logger.LogInformation("##deserialize object failure {0}", e);
                return null;
            }
            return userStore;
        }

        public void SetUser()
        {
            var user = _userService.CurrentUser;
            if (null == user)
            {
                return;
            }
            var userStore = new UserStore()
            {
                UserId = _userService.CurrentUser.Id,
                UserName = _userService.CurrentUser.UserName,
                Role = _userService.Role,
                Permissions = _userService.Permissions
            };
            var userStoreJsonString = JsonConvert.SerializeObject(userStore);
            var cookieSecurityString = _security.Encryption(userStoreJsonString);
            _httpContext.Response.Cookies.Append(COOKIE_NAME, cookieSecurityString, new CookieOptions()
            {
                Path = COOKIE_PATH,
                Expires = DateTimeOffset.Now.AddDays(7),
                HttpOnly = true,
                Domain = COOKIE_DOMAIN
            });
        }

    }
}
