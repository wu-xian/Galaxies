using Galaxies.Core.Services;
using Galaxies.Logic.BIZ;
using Galaxies.Model.EntityModel;
using Galaxies.Model.LogicModel;
using Galaxies.Model.ViewModel;
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
    public class UserController : ControllerBase
    {
        private UserBIZ userBIZ;
        private RoleBIZ roleBIZ;
        private ContextService contextService;
        public UserController(UserBIZ _userBIZ, RoleBIZ _roleBIZ, ContextService _contextService)
        {
            userBIZ = _userBIZ;
            roleBIZ = _roleBIZ;
            contextService = _contextService;
        }
        /// <summary>
        /// user manage home page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            UserCondition conditionModel = new UserCondition()
            {
                InTimeSDate = DateTime.MinValue,
                InTimeEDate = DateTime.Now,
                UserName = string.Empty
            };
            conditionModel.Roles = roleBIZ.RolsToSelectListItem();
            return View(conditionModel);
        }

        public IActionResult ModifyView(Guid userId)
        {
            int resultCount = 0;
            var user = userBIZ.GetUserWithRole(d => d.Id == userId, 0, 1, ref resultCount).FirstOrDefault();
            if (resultCount == 0) return OperationJson(RequestResultType.Failed, null, "操作失败");
            ModifyUser viewUser = new ModifyUser();
            viewUser.Email = user.Email;
            viewUser.RealName = user.RealName;
            viewUser.UserId = user.UserId.ToString();
            viewUser.UserName = user.UserName;
            viewUser.Roles = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            roleBIZ.Query(r => true)//有待优化 可以考虑使用缓存
                .ToList().ForEach(r =>
                {
                    viewUser.Roles.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                    {
                        Text = r.Name,
                        Value = r.Id.ToString(),
                        Selected = r.Id == user.RoleId
                    });
                });
            return PartialView(viewUser);
        }

        public IActionResult Modify(ModifyUser user)
        {
            var result = userBIZ.ModifyUser(user, contextService.User.Id);
            if (result == 0)
            {
                return OperationJson(RequestResultType.Failed, result, "修改失败");
            }
            return OperationJson(RequestResultType.Success, result, "修改成功");
        }

        public IActionResult PagingList(PagingRequestModel requestModel)
        {
            int count = 0;
            var result = userBIZ.GetUserWithRoleByUserAndRole((user, role) => true
            , requestModel.Offset
            , requestModel.Limit
            , ref count);
            return TableJson(result, count);
        }

        public IActionResult Query(PagingRequestModel requestModel, UserCondition condition)
        {
            int count = 0;
            var dbResult = userBIZ.GetUserWithRoleByUserAndRole((user, role) =>
            (
                (string.Equals(user.UserName, condition.UserName, StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrEmpty(condition.UserName)) &&
                (DateTime.Compare(user.InTime, condition.InTimeSDate) >= 0 &&
                DateTime.Compare(user.InTime, condition.InTimeEDate) <= 0) &&
                (role.Id == condition.RoleId || condition.RoleId == -1)
            )
            , requestModel.Offset
            , requestModel.Limit
            , ref count);

            return TableJson(dbResult, count);
        }

        public IActionResult Delete(Guid userId)
        {
            int dbResult = userBIZ.Delete(userId, contextService.User.Id);
            if (dbResult == 0)
            {
                return OperationJson(RequestResultType.Failed, null, "删除失败");
            }
            return OperationJson(RequestResultType.Success, dbResult, "删除成功");
        }

        public IActionResult Create(User user)
        {
            int dbResult = userBIZ.Create(user, contextService.User.Id);
            if (dbResult == 0)
            {
                return OperationJson(RequestResultType.Failed, null, "添加失败");
            }
            return OperationJson(RequestResultType.Failed, dbResult, "添加成功");
        }

        public IActionResult ChangeRole(Guid userId, int roleId)
        {
            int dbResult = userBIZ.ChangeUserRole(userId, roleId, contextService.User.Id);
            if (dbResult == 0)
            {
                return OperationJson(RequestResultType.Failed, null, "权限修改失败");
            }
            return OperationJson(RequestResultType.Failed, dbResult, "权限修改成功");
        }

        public IActionResult GetUser(Guid userId)
        {
            var usr = userBIZ.GetUserByUserId(userId);
            if (usr == null)
            {
                return OperationJson(RequestResultType.Failed, null, "无此用户");
            }
            return OperationJson(RequestResultType.Success, usr, "获取成功");
        }

    }
}
