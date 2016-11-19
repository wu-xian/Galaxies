using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Model.LogicModel
{
    public class PagingRequestModel
    {
        public int Limit { set; get; }
        public int Offset { set; get; }
        public string Order { set; get; }
    }
}
