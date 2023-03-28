using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class LoopSlotModel : BroadsignAPIModel
    {
        
        public string End_date { get; set; }
        public string Start_date { get; set; }
        public string Event_occurence { get; set; }
        public int Date_of_week_mask { get; set; }
        public int Domain_id { get; set; }
        public int Duration { get; set; }
        
        public int Inventory_category_id { get; set; }
        public int Priority { get; set; }
        public int Reps_per_hour { get; set; }

        //ID DEPENDENT !
        public static dynamic GetLoopSlot(string t, int du)
        {
            string path = "/loop_slot/v10/by_display_unit?display_unit_id=" + du;
            Requests.SendRequest(path, t, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }
    }
}
