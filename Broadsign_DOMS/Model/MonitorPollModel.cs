using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class MonitorPollModel : BroadsignAPIModel
    {
        public int Client_resource_id { get; set; }
        
        public int Domain_id { get; set; }
        public int Monitor_status { get; set; }
        public string Poll_last_utc { get; set; }
        public string Poll_next_expected_utc { get; set; }
        public string Private_ip { get; set; }
        public string Product_version { get; set; }
        public string Public_ip { get; set; }

        public static dynamic GetMonitorPoll(string t, int host = 0)
        {
            string path = "/monitor_poll/v2";
            Requests.SendRequest(path, t, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }

    }
}
