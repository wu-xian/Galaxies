using IDAL;
using Model.Context;
using Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.MySql
{
    public class ArticleDAL : BaseDAL<Article>, IArticleDAL
    {
        public ArticleDAL(LampDbContext _db)
        {
            db = _db;
        }
    }
}
