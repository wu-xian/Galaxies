using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galaxies.Model.EntityModel;

namespace Galaxies.Model.LogicModel
{
    public class UserStore
    {
        public Guid UserId { set; get; }
        public string UserName { set; get; }
        public Role Role { set; get; }
        public List<Permission> Permissions { set; get; }
    }
}
