using Galaxies.Logic.IDAL;
using Galaxies.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galaxies.Logic.BIZ
{
    public class UserRoleClaimBIZ
    {
        #region ctor&CRUD
        private IUserRoleClaimDAL useroleclaimDAL;
        public UserRoleClaimBIZ(IUserRoleClaimDAL _useroleclaimDAL)
        {
            useroleclaimDAL = _useroleclaimDAL;
        }

        public int Add(UserRoleClaim model)
        {
            return useroleclaimDAL.Add(model);
        }

        public int Modify(UserRoleClaim model)
        {
            return useroleclaimDAL.Modify(model);
        }

        public int Delete(UserRoleClaim model)
        {
            return useroleclaimDAL.Delete(model);
        }

        public IList<UserRoleClaim> GetPaging(Expression<Func<UserRoleClaim, bool>> whereLambda, int index, int pageSize)
        {
            return useroleclaimDAL.PagingList(whereLambda, index, pageSize);
        }

        public IList<UserRoleClaim> Query(Expression<Func<UserRoleClaim, bool>> whereLambda)
        {
            return useroleclaimDAL.Query(whereLambda);
        }
        #endregion

        public List<Role> GetRolesByUserId(Guid userId)
        {
            return useroleclaimDAL.Query(d => d.UserId == userId).Join(useroleclaimDAL.Raw.Role, urc => urc.RoleId, r => r.Id, (urc, r) => r).ToList();
        }

        public List<User> GetUserByRoleId(int roleId)
        {
            return useroleclaimDAL.Query(d => d.RoleId == roleId).Join(useroleclaimDAL.Raw.User, urc => urc.UserId, u => u.Id, (urc, u) => u).ToList();
        }
    }
}
