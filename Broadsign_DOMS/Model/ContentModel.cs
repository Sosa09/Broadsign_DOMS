using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class ContentModel
    {
        
        public int Approval_status { get; set; }
        public int Approved_by_user_id { get; set; }
        public string Approved_on_utc { get; set; }
        public int Archive_priority { get; set; }
        public int Archive_status { get; set; }
        public int Archived_by { get; set; }
        public string Archived_on_utc { get; set; }
        public string Attributes { get; set; }
        public int Bmb_host_id { get; set; }
        public string Checksum2 { get; set; }
        public int Checksum2_type { get; set; }
        
        public string Creation_tm { get; set; }
        public int Creation_user_id { get; set; }
        public int Domain_id { get; set; }
        public int External_id { get; set; }
        public string Feeds { get; set; }
        
        public string Mime { get; set; }
       
        public string Originalfilename { get; set; }
        
        public int Size { get; set; }
    }
}
