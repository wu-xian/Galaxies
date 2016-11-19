using Galaxies.Core.Services;
using Galaxies.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Controllers
{
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private ContextService contextService;
        public AccountController(ContextService _contextService)
        {
            contextService = _contextService;
        }

        [Route("/login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [Route("/loginAction")]
        public IActionResult Login(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (contextService.Login(model.UserName, model.Password))
                {
                    return OperationJson(RequestResultType.Success, new
                    {
                        statu = "redirect",
                        data = "/admin/user/index"
                    }, "登陆成功");
                }
                return OperationJson(RequestResultType.Failed, null, "登陆失败");
            }
            return OperationJson(RequestResultType.Forbiden, null, "登陆失败");
        }
    }
}
