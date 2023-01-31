using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class SkinSlotModel
    {
        public bool Active { get; set; }
        public string Day_mask { get; set; }
        public int Day_of_week_mask { get; set; }
        public int Domain_id { get; set; }
        public string End_or_deactivated_date { get; set; }
        public int Id { get; set; }
        public int Parent_id { get; set; }
        public int Skin_id { get; set; }
        public string Start_date { get; set; }
        public bool Temporary { get; set; }

        public static dynamic GetSkinSlot(string t, int du_id)
        {
            string path = "/skin_slot/v7/by_display_unit?display_unit_id=" + du_id;
            Requests.SendRequest(path, t, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }
    }
}
