using Galaxies.Logic.IDAL;
using Galaxies.Model.Context;
using Galaxies.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Logic.DAL.MYSQL
{
    public class RoleDAL : BaseDAL<Role>, IRoleDAL
    {
        public RoleDAL(GalaxiesDbContext _db)
        {
            db = _db;
        }
    }
}
