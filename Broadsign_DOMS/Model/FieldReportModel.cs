using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class FieldReportModel
    {
        public int Domain_id { get; set; }
        public int Id { get; set; }
        public int Target_resource_id { get; set; }
        public string Field_report { get; set; }
        public string Field_report_submitted_utc { get; set; }
        public string Player_capability_report { get; set; }
        public string Player_capability_report_submitted_utc { get; set; }

        //methods generate api result
    }
}
