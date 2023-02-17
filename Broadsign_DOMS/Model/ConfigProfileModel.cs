using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class ConfigProfileModel
    {
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Component_signature { get; set; }
        public string Configuration { get; set; }
        public string Max_version { get; set; }
        public string Min_version { get; set; }
        public int Id { get; set; }
        public int Parent_id { get; set; }
        public int Domain_id { get; set; }

    }
}
