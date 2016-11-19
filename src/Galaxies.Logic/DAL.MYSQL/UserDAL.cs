using Galaxies.Logic.IDAL;
using Galaxies.Model.Context;
using Galaxies.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Logic.DAL.MYSQL
{
    public class UserDAL : BaseDAL<User>, IUserDAL
    {
        public UserDAL(GalaxiesDbContext _db)
        {
            db = _db;
        }
    }
}
