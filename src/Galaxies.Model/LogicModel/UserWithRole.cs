using Galaxies.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Model.LogicModel
{
    public class UserWithRole
    {
        public Guid UserId { set; get; }
        public string UserName { set; get; }
        public string RealName { set; get; }
        public string PhoneNo { set; get; }
        public string Email { set; get; }
        public int LoginTimes { set; get; }
        public DateTime InTime { set; get; }
        public int RoleId { set; get; }
        public string RoleName { set; get; }
        public string RoleDescription { set; get; }
    }
}
