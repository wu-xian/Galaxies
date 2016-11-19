using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Model.LogicModel
{
    public class PagingResponseModel
    {
        public object Rows { set; get; }
        public int Total { set; get; }
    }
}
