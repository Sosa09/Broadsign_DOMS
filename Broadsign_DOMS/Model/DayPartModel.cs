using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class DayPartModel
    {
        
       
        public string End_date { get; set; }
        public string End_time { get; set; }
        public string Minute_mask { get; set; }
        public string Start_date { get; set; }
        public string Start_time { get; set; }
        public string Virtual_end_date { get; set; }
        public string Virtual_start_date { get; set; }
        
        public int Day_mask { get; set; }
        public int Domain_id { get; set; }
        public int Impressions_per_hour { get; set; }
        
        public int Weight { get; set; }

        //methods get Day parts

    }
}
