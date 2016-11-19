using System;

namespace Galaxies.Model.EntityModel
{
    public class ProgramRoleClaim
    {
        public int Id { set; get; }
        public int ProgramId { set; get; }
        public int RoleId { set; get; }

        public bool InUse { set; get; }
        public DateTime InTime { set; get; }
        public DateTime ModifyTime { set; get; }
        public Guid ModifyUser { set; get; }
    }
}