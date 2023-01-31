
using Newtonsoft.Json;

namespace Broadsign_DOMS.Model
{
    public class FrameModel
    {
        public bool Active { get; set; }
        public int Domain_id { get; set; }
        public int Geometry_type { get; set; }
        public int Id { get; set; }
        public int Interactivity_timeout { get; set; }
        public int Interactivity_trigger_id { get; set; }
        public int Loop_policy_id { get; set; }
        public string Name { get; set; }
        public int Parent_id { get; set; }
        public int Screen_no { get; set; }
        public int Height{ get; set; }
        public int Width{ get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public static dynamic GetFrames(string t, int id = 0)
        {
            string path = "/skin/v7";
            Requests.SendRequest(path, t, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }
    }
}
