using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class ConfigProfileModel
    {
        
       
        public string Component_signature { get; set; }
        public string Configuration { get; set; }
        public string Max_version { get; set; }
        public string Min_version { get; set; }
        
        
        public int Domain_id { get; set; }

    }
}
