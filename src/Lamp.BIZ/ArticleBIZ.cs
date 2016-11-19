using DAL.MySql;
using Galaxies.Model.EntityModel;
using IDAL;
using Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lamp.BIZ
{
    public class ArticleBIZ
    {
        private IArticleDAL articleDAL;
        public ArticleBIZ(IArticleDAL _articleDAL)
        {
            articleDAL = _articleDAL;
        }

        public IList<Article> Paging(int index, int pageSize, ref int count)
        {
            return articleDAL.PagingList(d => true, d => d.Id, index, pageSize, ref count);
        }
    }
}
