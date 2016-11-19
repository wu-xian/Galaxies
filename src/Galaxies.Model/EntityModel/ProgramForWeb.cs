using System;

namespace Galaxies.Model.EntityModel
{
    public class  ProgramForWeb
    {
        public int Id { set; get; }
        public string Description { set; get; }//程序描述
        public string AreaName { set; get; }//区域名
        public string ControllerName { set; get; }//控制器名
        public string ActionName { set; get; }//方法名
        public int Method { set; get; }//请求方式
        public string Params { set; get; }//参数
        public string ModuleName { set; get; }//模块名
        public DateTime InTime { set; get; }//添加时间
        public DateTime ModifyTime { set; get; }//修改时间
        public Guid ModifyUser { set; get; }//修改人
        public bool InUse { set; get; }//是否使用
    }
}