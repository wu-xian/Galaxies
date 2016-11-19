using Galaxies.Logic.IDAL;
using Galaxies.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galaxies.Logic.BIZ
{
    public class ProgramRoleClaimBIZ
    {
        #region ctor&CRUD
        private IProgramRoleClaimDAL programroleclaimDAL;
        private IUserRoleClaimDAL userroleclaimDAL;
        public ProgramRoleClaimBIZ(IProgramRoleClaimDAL _programroleclaimDAL
            , IUserRoleClaimDAL _userroleclaimDAL)
        {
            programroleclaimDAL = _programroleclaimDAL;
            userroleclaimDAL = _userroleclaimDAL;
        }

        public int Add(ProgramRoleClaim model)
        {
            return programroleclaimDAL.Add(model);
        }

        public int Modify(ProgramRoleClaim model)
        {
            return programroleclaimDAL.Modify(model);
        }

        public int Delete(ProgramRoleClaim model)
        {
            return programroleclaimDAL.Delete(model);
        }

        public IList<ProgramRoleClaim> GetPaging(Expression<Func<ProgramRoleClaim, bool>> whereLambda, int index, int pageSize)
        {
            return programroleclaimDAL.PagingList(whereLambda, index, pageSize);
        }

        public IList<ProgramRoleClaim> Query(Expression<Func<ProgramRoleClaim, bool>> whereLambda)
        {
            return programroleclaimDAL.Query(whereLambda);
        }
        #endregion

        public List<ProgramForWeb> GetProgramsByRoleId(int roleId)
        {
            return programroleclaimDAL.Query(d => d.RoleId == roleId)
                .Join(programroleclaimDAL.Raw.ProgramForWeb
                , prc => prc.RoleId
                , pfw => pfw.Id
                , (prc, pfw) => pfw)
                .ToList();
        }

        public List<Role> GetRolesByProgramId(int programId)
        {
            return programroleclaimDAL.Query(d => d.ProgramId == programId)
                .Join(programroleclaimDAL.Raw.Role
                , prc => prc.ProgramId
                , r => r.Id, (prc, r) => r)
                .ToList();
        }

        public List<ProgramForWeb> GetWebProgramByUserId(Guid userId)
        {
            return userroleclaimDAL.Query(d => d.UserId == userId)
                .Join(userroleclaimDAL.Raw.Role
                , urc => urc.RoleId
                , r => r.Id, (urc, r) => r)//result roles
                .Join(userroleclaimDAL.Raw.ProgramRoleClaim
                , r => r.Id, prc => prc.RoleId
                , (r, prc) => prc)//result programroleclaim
                .Join(userroleclaimDAL.Raw.ProgramForWeb
                , prc => prc.ProgramId
                , pfw => pfw.Id
                , (prc, pfw) => pfw)//result programForWeb
                .ToList();
        }
    }
}
