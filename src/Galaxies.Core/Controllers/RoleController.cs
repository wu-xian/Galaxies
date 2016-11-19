using Galaxies.Core.Services;
using Galaxies.Logic.BIZ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class RoleController : ControllerBase
    {
        private UserBIZ userBIZ;
        private RoleBIZ roleBIZ;
        private ProgramForWebBIZ programForWebBIZ;
        private ContextService contextService;

        public RoleController(ContextService _contextService, UserBIZ _userBIZ, RoleBIZ _roleBIZ, ProgramForWebBIZ _programForWebBIZ)
        {
            contextService = _contextService;
            userBIZ = _userBIZ;
            roleBIZ = _roleBIZ;
            programForWebBIZ = _programForWebBIZ;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            int count = 0;
            var result = roleBIZ.Query(d => true).Select(d => new
            {
                id = d.Id,
                name = d.Name,
                description = d.Description,
                inTime = d.InTime
            });
            return TableJson(result, 50);
        }

        public IActionResult ProgramForWeb(int roleId)
        {
            return Json(programForWebBIZ.GetByRoleId(roleId).Select(d => new
            {
                Area = d.AreaName,
                Controller = d.ControllerName,
                Action = d.ActionName
            }));
        }
    }
}
