using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Authorization
{
    public class AuthAttribute : Attribute
    {
        private List<string> _permission;
        public List<string> Permission
        {
            get
            {
                return _permission;
            }
        }

        public AuthAttribute(params string[] permission)
        {
            if (permission == null || permission.Count() == 0)
            {
                throw new ArgumentNullException(nameof(permission));
            }
            _permission = permission.ToList();
        }

        public AuthAttribute()
        {

        }
    }
}
