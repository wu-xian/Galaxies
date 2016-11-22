using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Model.EntityModel
{
    public class Permission
    {
        public int Id { set; get; }
        public int Name { set; get; }
        public string DisplayName { set; get; }
        public int RoleId { set; get; }
        public bool InUse { set; get; }

    }
}
