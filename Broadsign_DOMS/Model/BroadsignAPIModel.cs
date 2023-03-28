using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class BroadsignAPIModel
    {
        public bool Active { get; set; }
        public int Container_id { get; set; }
        public int Domain_id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent_id { get; set; }
    }
}
