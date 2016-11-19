using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.EntityModel
{
    public class RoomMsg
    {
        public int RoomId { set; get; }
        public Guid SenderId { set; get; }
        public string Content { set; get; }
        public bool InTime { set; get; }
    }
}
