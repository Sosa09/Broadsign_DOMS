using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class DisplayUnitModel
    {
        
        public string address { get; set; }
        public int bmb_host_id { get; set; }
        public int display_unit_type_id { get; set; }
        public int domain_id { get; set; }
        public bool enforce_day_parts { get; set; }
        public bool enforce_screen_controls { get; set; }
        public bool export_enabled { get; set; }
        public int export_first_enabled_by_user_id { get; set; }
        public string export_first_enabled_tm { get; set; }
        public int export_retired_by_user_id { get; set; }
        public string export_retired_on_tm { get; set; }
        public string external_id { get; set; }
        public string geolocation { get; set; }
        public int host_screen_count { get; set; }
        public string timezone { get; set; }
        public int virtual_host_screen_count { get; set; }
        public string virtual_id { get; set; }
        public string zipcode { get; set; }

        public static dynamic GetDisplayUnits(string token, int id = 0)
        {
            string path = "display_unit/v12";
            Requests.SendRequest(path, token, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject<ObservableCollection<DisplayUnitModel>>(Requests.Response.Content);
        }
    }
}
