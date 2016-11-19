using Galaxies.Logic.IDAL;
using Galaxies.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galaxies.Logic.BIZ
{
    public class MenuForWebInRolesBIZ
    {
        #region ctor&CRUD
        private IMenuForWebInRolesDAL menuforwebinrolesDAL;
        public MenuForWebInRolesBIZ(IMenuForWebInRolesDAL _menuforwebinrolesDAL)
        {
            menuforwebinrolesDAL = _menuforwebinrolesDAL;
        }

        public int Add(MenuForWebInRoles model)
        {
            return menuforwebinrolesDAL.Add(model);
        }

        public int Modify(MenuForWebInRoles model)
        {
            return menuforwebinrolesDAL.Modify(model);
        }

        public int Delete(MenuForWebInRoles model)
        {
            return menuforwebinrolesDAL.Delete(model);
        }

        public IList<MenuForWebInRoles> GetPaging(Expression<Func<MenuForWebInRoles, bool>> whereLambda, int index, int pageSize)
        {
            return menuforwebinrolesDAL.PagingList(whereLambda, index, pageSize);
        }

        public IList<MenuForWebInRoles> Query(Expression<Func<MenuForWebInRoles, bool>> whereLambda)
        {
            return menuforwebinrolesDAL.Query(whereLambda);
        }
        #endregion
    }
}
