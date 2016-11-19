using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return Json(new { message = "im admin admin index" });
        }
    }
}