using Galaxies.Core.Services;
using Galaxies.Model.LogicModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Galaxies.Core.Filter
{
    public class GlobalActionFilter : TypeFilterAttribute
    {
        public GlobalActionFilter() : base(typeof(ActionFilter))
        {
        }

        private class ActionFilter : IActionFilter
        {
            private ContextService contextService;
            private SessionService sessionService;
            public ActionFilter(ContextService _contextService, SessionService _session)
            {
                contextService = _contextService;
                sessionService = _session;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {

            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var controllerDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var controllerAttributes = controllerDescriptor.ControllerTypeInfo.CustomAttributes?.ToList();
                var actionAttrubutes = controllerDescriptor.MethodInfo.CustomAttributes?.ToList();
                if (!CustomAttributesHasType(controllerAttributes, typeof(AllowAnonymousAttribute)) && !CustomAttributesHasType(actionAttrubutes, typeof(AllowAnonymousAttribute)))
                {
                    if (!contextService.IsLogin())
                    {
                        if (!contextService.GetCookie())
                        {
                            Forbiden(context);
                            return;
                        }
                    }

                    string area = context.ActionDescriptor.RouteValues["area"];
                    string controller = context.ActionDescriptor.RouteValues["controller"];
                    string action = context.ActionDescriptor.RouteValues["action"];
                    if (!contextService.HasPermission(area, controller, action))
                    {
                        Forbiden(context);
                        return;
                    }
                }
            }

            private bool CustomAttributesHasType(List<CustomAttributeData> list, Type type)
            {
                if (list != null && list.Count() != 0)
                {
                    foreach (var item in list)
                    {
                        if (item.AttributeType.Equals(type))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            private void Forbiden(ActionExecutingContext context)
            {
                var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (isAjax)
                {
                    context.Result = new JsonResult(new OperationResponseModel()
                    {
                        Result = DescriptionHelper.GetDescription(RequestResultType.Forbiden),
                        Message = "权限禁止",
                        Data = "/login"
                    });
                    return;
                }
                context.Result = new ForbidResult();
                return;
            }

        }
    }
}
