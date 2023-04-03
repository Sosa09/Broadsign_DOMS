
using Broadsign_DOMS.Resource;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Broadsign_DOMS.Model
{
    public class FrameModel : BroadsignAPIModel
    {
        
        public int Domain_id { get; set; }
        public int Geometry_type { get; set; }
        
        public int Interactivity_timeout { get; set; }
        public int Interactivity_trigger_id { get; set; }
        public int Loop_policy_id { get; set; }
       
        public string NewName { get; set; }
        public int Screen_no { get; set; }
        public int Height{ get; set; }
        public int Width{ get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        
       

        private static dynamic _getFrames(string t, int id = 0)
        {
            string path = "/skin/v7";
            Requests.SendRequest(path, t, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }

        public async static Task GenerateFrmaes(string t)
        {
            await Task.Delay(1);
            try
            {
                dynamic frames = _getFrames(t);
                if (frames != null)
                {
                    foreach (var frame in frames["skin"])
                    {
                        if (frame.active == true)
                        {
                            CommonResources.Frames.Add(new FrameModel
                            {
                                Active = frame.active,
                                Domain_id = frame.domain_id,
                                Geometry_type = frame.geometry_type,
                                Height = frame.height,
                                Id = frame.id,
                                Interactivity_timeout = frame.interactivity_timeout,
                                Interactivity_trigger_id = frame.interactivity_trigger_id,
                                Loop_policy_id = frame.loop_policy_id,
                                Name = frame.name,
                                Parent_id = frame.parent_id,
                                Screen_no = frame.screen_no,
                                Width = frame.width,
                                X = frame.x,
                                Y = frame.y,
                                Z = frame.z
                            });
                        }

                    }
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }


        }
    }
}
