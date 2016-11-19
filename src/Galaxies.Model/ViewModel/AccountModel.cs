using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Model.ViewModel
{
    public class AccountModel
    {
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        [DataType(DataType.Text)]
        public string UserName { set; get; }
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        //public bool RememberMe { set; get; }
    }
}
