using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.EntityModel
{
    public class Room
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public DateTime InTime { set; get; }
        public bool InUse { set; get; }
    }
}
