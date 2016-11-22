using Galaxies.Model.EntityModel;
using Galaxies.Model.LogicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Services
{
    public class UserService
    {
        private User _user;
        private Role _role;
        private List<Permission> _permission;

        public UserService()
        {
            _user = new User();
            _role = new Role();
            _permission = new List<Permission>();
        }
        public void LoadUser(User user, Role role, List<Permission> permissions)
        {
            _user = user;
            _role = role;
            _permission = permissions;
        }

        public void LoadUser(UserStore userStore)
        {
            if (null == userStore)
                throw new ArgumentNullException(nameof(userStore));
            _user.Id = userStore.UserId;
            _user.UserName = userStore.UserName;
            _role = userStore.Role;
            _permission = userStore.Permissions;
        }

        public User CurrentUser
        {
            get
            {
                return _user;
            }
        }

        public Role Role
        {
            get
            {
                return _role;
            }
        }

        public List<Permission> Permissions
        {
            get
            {
                return _permission;
            }
        }

    }
}
