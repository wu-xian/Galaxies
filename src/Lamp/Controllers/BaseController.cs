using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lamp.Controllers
{
    public class BaseController:Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string controllerName = context.ActionDescriptor.RouteValues["controller"];
            string actionName = context.ActionDescriptor.RouteValues["action"];
            string method = context.HttpContext.Request.Method;
            //base.OnActionExecuting(context);
        }

        public JsonResult Json(ResultStatu statu,string cmd,string param,int resultCode=13579)
        {
            return Json(new {
                statu=statu,
                cmd=cmd,
                param=param,
                resultCode=resultCode
            });
        }
    }

    public enum ResultStatu
    {
        Success,
        Faild
    }
}
