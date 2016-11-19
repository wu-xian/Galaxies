using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.LogicModel;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galaxies.Core.Services;

namespace Lamp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private ContextService contextService;
        public AccountController(ContextService _contextService)
        {
            contextService = _contextService;
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
