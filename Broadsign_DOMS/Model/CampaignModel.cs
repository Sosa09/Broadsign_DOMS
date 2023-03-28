using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class CampaignModel
    {
        
        public bool Auto_synchronize_bundles { get; set; }
        public bool Contract_fields_read_only { get; set; }
        public bool Default_fullscreen { get; set; }
        public bool Has_goal { get; set; }
        
        public int State { get; set; }
        public int Bmb_host_id { get; set; }
        public int Booking_state { get; set; }
        
        public int Creation_user_id { get; set; }
        public int Day_of_week_mask { get; set; }
        public int Default_bundle_weight { get; set; }
        public int Default_category_id { get; set; }
        public int Default_interactivity_timeout { get; set; }
        public int Default_interactivity_trigger_id { get; set; }
        public int Default_schedule_id { get; set; }
        public int Default_segment_category_id { get; set; }
        public int Default_trigger_category_id { get; set; }
        public int Domain_id { get; set; }
        public int Duration_msec { get; set; }
        public int Estimated_reps { get; set; }
        public int Goal_amount { get; set; }
        public int Goal_unit { get; set; }
        public int Media_package_id { get; set; }
        public int Pacing_period { get; set; }
        public int Pacing_target { get; set; }
        
        public int Promoter_user_id { get; set; }
        public int Saturation { get; set; }
       
        public string Booking_state_calculated_on { get; set; }
        public string Contract_id { get; set; }
        public string Creation_tm { get; set; }
        public string Default_attributes { get; set; }
        public string Default_secondary_sep_category_ids { get; set; }
        public string End_date { get; set; }
        public string End_time { get; set; }
        public string Goal_reached_on_tm { get; set; }
        public string Promotion_time { get; set; }
        public string Proposal_id { get; set; }
        public string Proposal_line_item_id { get; set; }
        public string Reps_calculated_on { get; set; }
        public string Start_date { get; set; }
        public string Start_time { get; set; }

        public static dynamic GetCampaigns(string t, int id = 0)
        {
            string path = "/reservation/v22";
            Requests.SendRequest(path, t, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }
    }
}
