using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.EntityModel
{
    public class Repertory
    {
        public Guid ArticleId { set; get; }
        public int Number { set; get; }
        public byte[] DataValue { set; get; }
        public long DataLength { set; get; }
        public string DataType { set; get; }
        public string DataName { set; get; }
        public DateTime InTime { set; get; }
        public Guid Creator { set; get; }
    }
}
