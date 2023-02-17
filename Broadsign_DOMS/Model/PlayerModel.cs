using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class PlayerModel
    {
        public string Active { get; set; }
        public int Config_profile_bag_id { get; set; }
        public int Container_id { get; set; }
        public string Custom_unique_id { get; set; }
        public string Db_pickup_tm_utc { get; set; }
        public int Discovery_status { get; set; }
        public int Display_unit_id { get; set; }
        public int Domain_id { get; set; }
        public string Geolocation { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Nscreens { get; set; }
        public string Primary_mac_address { get; set; }
        public string Public_key_fingerprint { get; set; }
        public string Remote_clear_db_tm_utc { get; set; }
        public string Remote_reboot_tm_utc { get; set; }
        public string Secondary_mac_address { get; set; }
        public int Volume { get; set; }

    }
}
