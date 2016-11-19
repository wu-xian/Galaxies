using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Model.EntityModel
{
    public class MenuForWebInRoles
    {
        public int Id { set; get; }
        public int MenuId { set; get; }
        public int RoleId { set; get; }
        public bool InUse { set; get; }
        public DateTime InTime { set; get; }
    }
}
