using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Authorization
{
    public static class AuthConstants
    {
        //Admin User
        public static string Admin_User_Create = "Admin.User.Create";
        public static string Admin_User_Update = "Admin.User.Update";
        public static string Admin_User_Read = "Admin.User.Read";
        public static string Admin_User_Delete = "Admin.User.Delete";

        //Admin Role
        public static string Admin_Role_Create = "Admin.Role.Create";
        public static string Admin_Role_Update = "Admin.Role.Update";
        public static string Admin_Role_Read = "Admin.Role.Read";
        public static string Admin_Role_Delete = "Admin.Role.Delete";
    }
}
