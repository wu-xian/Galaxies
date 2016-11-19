using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Model.EntityModel
{
    public class MenuForWeb
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Href { set; get; }
        public bool InUse { set; get; }
        public DateTime InTime { set; get; }
        public char OpenType { set; get; }
    }
}
