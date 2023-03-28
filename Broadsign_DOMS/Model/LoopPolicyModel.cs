using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class LoopPolicyModel : BroadsignAPIModel
    {
        
       
        
        public string Attribute { get; set; }
        
        public int Default_slot_duration { get; set; }
        public int Domain_id { get; set; }
        public int Filler_maximum_unique_content { get; set; }
        public string Loop_share_configuration { get; set; }
        public string Loop_transform_strategy { get; set; }
        public int Max_duration_msec { get; set; }
        public bool Overbookable { get; set; }
        public int Pirmary_inventory_share_msec { get; set; }
        public string Synchronization_set { get; set; }
        public int Synchronization_type { get; set; }

        public static dynamic GetLoopPolicy(string t, int id = 0)
        {
            string path = "/loop_policy/v10";
            Requests.SendRequest(path, t, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }

    }
}
