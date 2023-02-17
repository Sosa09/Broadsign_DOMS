using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class BundleModel
    {
        public string Name { get; set; }
        public string Secondary_sep_category_ids { get; set; }
        public string Attributes { get; set; }
        public string Loop_positions { get; set; }
        public int Category_id { get; set; }
        public int Container_id { get; set; }
        public int Domain_id { get; set; }
        public int Id { get; set; }
        public int Interactivity_timeout { get; set; }
        public int Interactivity_trigger_id { get; set; }
        public int Loop_category_id { get; set; }
        public int Loop_weight { get; set; }
        public int Max_duration_msec { get; set; }
        public int Parent_id { get; set; }
        public int Position { get; set; }
        public int Trigger_category_id { get; set; }
        public bool Active { get; set; }
        public bool Allow_custom_duration { get; set; }
        public bool Auto_synchronized { get; set; }
        public bool Fullscreen { get; set; }
    }
}
