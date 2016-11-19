using Galaxies.Logic.IDAL;
using Galaxies.Model.EntityModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galaxies.Logic.BIZ
{
    public class RoleBIZ
    {
        #region ctor&CRUD
        private IRoleDAL roleDAL;
        public RoleBIZ(IRoleDAL _roleDAL)
        {
            roleDAL = _roleDAL;
        }

        public int Add(Role model)
        {
            return roleDAL.Add(model);
        }

        public int Modify(Role model)
        {
            return roleDAL.Modify(model);
        }

        public int Delete(Role model)
        {
            return roleDAL.Delete(model);
        }

        public IList<Role> GetPaging(Expression<Func<Role, bool>> whereLambda, int index, int pageSize)
        {
            return roleDAL.PagingList(whereLambda, index, pageSize);
        }

        public IList<Role> Query(Expression<Func<Role, bool>> whereLambda)
        {
            return roleDAL.Query(whereLambda);
        }
        #endregion

        public List<SelectListItem> RolsToSelectListItem()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem()
            {
                Value = "-1",
                Text = "全部",
                Selected = true
            });
            roleDAL.All().ToList().ForEach(d =>
            {
                items.Add(new SelectListItem()
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                });
            });
            return items;
        }

        public List<Role> GetPaging(Expression<Func<Role, bool>> whereLambda, int index, int size, ref int count)
        {
            return roleDAL.PagingList(whereLambda, d => d.Id, index, size, ref count).ToList();
        }

        public int Modify(Expression<Func<Role, bool>> whereLambda, Role _role, Guid operatorId)
        {
            return roleDAL.Modify(whereLambda, role =>
            {
                role.Description = _role.Description;
                role.ModifyTime = DateTime.Now;
                role.ModifyUser = operatorId;
                role.Name = _role.Name;
                role.ParentId = role.ParentId;
            });
        }

        public List<KeyValuePair<int, string>> GetParentList(int roleId)
        {
            List<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();
            int parentId = roleDAL.Query(d => d.Id == roleId).Select(d => d.ParentId).FirstOrDefault();
            var parentResult = roleDAL.Query(d => d.Id == parentId)?.FirstOrDefault();
            while (parentResult != null && parentId != 0)
            {
                result.Add(new KeyValuePair<int, string>(parentResult.Id, parentResult.Name));
                parentResult = roleDAL.Query(d => d.Id == parentResult.ParentId)?.FirstOrDefault();
            }
            return result;
        }

        public int Delete(int roleId, Guid operatorId)
        {
            return roleDAL.Modify(d => d.Id == roleId, roles =>
            {
                roles.InUse = false;
                roles.ModifyTime = DateTime.Now;
                roles.ModifyUser = operatorId;
            });
        }

        public int Add(Role role, Guid operatorId)
        {
            role.Id = 0;
            role.InTime = DateTime.Now;
            role.InUse = true;
            role.ModifyTime = DateTime.Now;
            role.ModifyUser = operatorId;
            return roleDAL.Add(role);
        }
    }
}
