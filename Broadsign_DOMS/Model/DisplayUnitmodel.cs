using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class DisplayUnitmodel
    {
        
        public string Address { get; set; }
        public int Bmb_host_id { get; set; }
        
        public int Display_unit_type_id { get; set; }
        public int Domain_id { get; set; }
        public bool Enforce_day_parts { get; set; }
        public bool Enforce_screen_controls { get; set; }
        public bool Export_enabled { get; set; }
        public int Export_first_enabled_by_user_id { get; set; }
        public string Export_first_enabled_tm { get; set; }
        public int Export_retired_by_user_id { get; set; }
        public string Export_retired_on_tm { get; set; }
        public string External_id { get; set; }
        public string Geolocation { get; set; }
        public int Host_screen_count { get; set; }
        
       
        public string Timezone { get; set; }
        public int Virtual_host_screen_count { get; set; }
        public string Virtual_id { get; set; }
        public string Zipcode { get; set; }
    }
}
