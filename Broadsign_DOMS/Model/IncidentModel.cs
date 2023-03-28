using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class IncidentModel : BroadsignAPIModel
    {
        public string Critical_escalation_tm_utc { get; set; }
        public string last_status_code_change_utc { get; set; }
        public string Occurred_on { get; set; }
        public string Problem_description { get; set; }
        public string Reported_on { get; set; }
        public string Resolved_description { get; set; }
        public string Resolved_on { get; set; }
        public string Warning_escalation_tm_utc { get; set; }
        public int Domain_id { get; set; }
        public int Escalation_status { get; set; }
        
        public int Resource_id { get; set; }
        public int Status_code { get; set; }
        public int Target_resource_id { get; set; }
        public int Type_code { get; set; }

        public static dynamic GetIncidents(string t)
        {
            string path = "";
            Requests.SendRequest(path, t, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }

    }
}
