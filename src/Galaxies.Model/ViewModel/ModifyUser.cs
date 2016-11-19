using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Model.ViewModel
{
    public class ModifyUser
    {
        public string UserId { set; get; }
        public int RoleId { set; get; }
        public List<SelectListItem> Roles { set; get; }
        public string RealName { set; get; }
        public string Email { set; get; }
        public string UserName { set; get; }
    }
}
