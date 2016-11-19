using Galaxies.Logic.IDAL;
using Galaxies.Model.Context;
using Galaxies.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galaxies.Logic.BIZ
{
    public class ProgramForWebBIZ
    {
        #region ctor&CRUD
        private IProgramForWebDAL programforwebDAL;
        private IProgramRoleClaimDAL programRoleClaimDAL;
        private GalaxiesDbContext db;
        public ProgramForWebBIZ(IProgramForWebDAL _programforwebDAL, IProgramRoleClaimDAL _programRoleClaimDAL, GalaxiesDbContext _db)
        {
            programforwebDAL = _programforwebDAL;
            programRoleClaimDAL = _programRoleClaimDAL;
            db = _db;
        }

        public int Add(ProgramForWeb model)
        {
            return programforwebDAL.Add(model);
        }

        public int Modify(ProgramForWeb model)
        {
            return programforwebDAL.Modify(model);
        }

        public int Delete(ProgramForWeb model)
        {
            return programforwebDAL.Delete(model);
        }

        public IList<ProgramForWeb> GetPaging(Expression<Func<ProgramForWeb, bool>> whereLambda, int index, int pageSize)
        {
            return programforwebDAL.PagingList(whereLambda, index, pageSize);
        }

        public IList<ProgramForWeb> Query(Expression<Func<ProgramForWeb, bool>> whereLambda)
        {
            return programforwebDAL.Query(whereLambda);
        }
        #endregion

        public IList<ProgramForWeb> GetPaging(Expression<Func<User, bool>> whereLambda, int index, int size, ref int count)
        {
            return programforwebDAL.PagingList(d => true, d => d.Id, index, size, ref count);
        }

        public List<ProgramForWeb> GetByRoleId(int roleId)
        {
            var dbResult = from program in db.ProgramForWeb
                           join programroleclaim in db.ProgramRoleClaim on program.Id equals programroleclaim.ProgramId
                           where programroleclaim.RoleId == roleId
                           select program;
            return dbResult.ToList();
        }
    }
}
