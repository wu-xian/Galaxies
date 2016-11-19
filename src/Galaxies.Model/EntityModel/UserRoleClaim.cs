using System;

namespace Galaxies.Model.EntityModel
{
    public class UserRoleClaim
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }//用户Id
        public int RoleId { set; get; }//权限Id
        public bool InUse { set; get; }//是否使用
        public DateTime InTime { set; get; }//录入时间
        public DateTime ModifyTime { set; get; }//修改时间
        public Guid ModifyUser { set; get; }//修改人Id
    }
}