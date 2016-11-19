using System;

namespace Galaxies.Model.EntityModel
{
    public class User
    {
        public Guid Id { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string RealName { set; get; }
        public string PhoneNo { set; get; }
        public string Email { set; get; }
        public bool InUse { set; get; }
        public DateTime InTime { set; get; }
        public DateTime ModifyTime { set; get; }
        public Guid ModifyUser { set; get; }
        public int LoginTimes { set; get; }
    }
}
