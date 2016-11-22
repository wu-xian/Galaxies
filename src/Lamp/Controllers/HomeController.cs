using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DAL.MySql;
using System.Text;
using Galaxies.Core.Services;
using Microsoft.Extensions.Options;
using Galaxies.Model.LogicModel;
using Microsoft.Extensions.Configuration;

namespace Lamp.Controllers
{
    public class HomeController : BaseController
    {
        private SecurityService se;
        private ContextService contextService;
        private SecurityService security;
        public HomeController(SecurityService _se, ContextService con, SecurityService _security)
        {
            se = _se;
            contextService = con;
            security = _security;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Get()
        {
            return Json(new
            {
                faild = "false"
            });
        }

        //public IActionResult About()
        //{
        //    var db = new LampDbContext();
        //    //var setter = dbSession.DbContext.Set<User>();
        //    var result = db.User.Where(d => true).ToList();
        //    return Json(new
        //    {
        //        result = result
        //    });
        //}

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return Content("im Admin");
        }

        public IActionResult System()
        {
            return Content("im System");
        }

        public IActionResult Roles()
        {
            return Content("im Roles");
        }

        public IActionResult Set()
        {
            HttpContext.Items.Add("aa", "bbb");
            return Content((string)HttpContext.Items["aa"]);
        }

        public IActionResult Get1()
        {
            return Json(HttpContext.Items["aa"]);
        }

    }
}
