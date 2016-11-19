using Dapper;
using IDAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.MySql
{
    public class BaseDAL<T> : Galaxies.Logic.DAL.MYSQL.BaseDAL<T>,Galaxies.Logic.IDAL.IBaseDAL<T> where T : class, new()
    {
    }
}
