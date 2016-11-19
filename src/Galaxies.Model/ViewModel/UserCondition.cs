using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Model.ViewModel
{
    public class UserCondition
    {
        [DataType(DataType.Text)]
        public string UserName { set; get; }
        [DataType(DataType.Date)]
        public DateTime InTimeSDate { set; get; }
        [DataType(DataType.Date)]
        public DateTime InTimeEDate { set; get; }
        public int RoleId { set; get; }
        public List<SelectListItem> Roles { set; get; }

    }
}
