using Galaxies.Logic.IDAL;
using Galaxies.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galaxies.Logic.BIZ
{
    public class MenuForWebBIZ
    {
        #region ctor&CRUD
        private IMenuForWebDAL menuforwebDAL;
        public MenuForWebBIZ(IMenuForWebDAL _menuforwebDAL)
        {
            menuforwebDAL = _menuforwebDAL;
        }

        public int Add(MenuForWeb model)
        {
            return menuforwebDAL.Add(model);
        }

        public int Modify(MenuForWeb model)
        {
            return menuforwebDAL.Modify(model);
        }

        public int Delete(MenuForWeb model)
        {
            return menuforwebDAL.Delete(model);
        }

        public IList<MenuForWeb> GetPaging(Expression<Func<MenuForWeb, bool>> whereLambda, int index, int pageSize)
        {
            return menuforwebDAL.PagingList(whereLambda, index, pageSize);
        }

        public IList<MenuForWeb> Query(Expression<Func<MenuForWeb, bool>> whereLambda)
        {
            return menuforwebDAL.Query(whereLambda);
        }
        #endregion
    }
}
