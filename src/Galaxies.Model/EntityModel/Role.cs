using System;

namespace Galaxies.Model.EntityModel
{
    public class Role
    {
        public int Id { set; get; }
        public int ParentId { set; get; }//父权限Id
        public string Name { set; get; }//权限名称
        public string Description { set; get; }//权限描述
        public bool InUse { set; get; }//是否使用
        public DateTime InTime { set; get; }//添加时间
        public DateTime ModifyTime { set; get; }//操作时间
        public Guid ModifyUser { set; get; }//修改人
    }
}