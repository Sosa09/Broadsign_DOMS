using Newtonsoft.Json;
using System.Windows;

namespace Broadsign_DOMS.Model
{
    public class ContainerModel
    {
        private bool active;
        private int container_id;
        private int domain_id;
        private int group_id;
        private int id;
        private string name;
        private int parent_id;
        private string parent_resource_type;

        public bool Active { get => active; set => active = value; }
        public int Container_id { get => container_id; set => container_id = value; }
        public int Domain_id { get => domain_id; set => domain_id = value; }
        public int Group_id { get => group_id; set => group_id = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Parent_id { get => parent_id; set => parent_id = value; }
        public string Parent_resource_type { get => parent_resource_type; set => parent_resource_type = value; }


        public static dynamic GetContainers(string token, int id = 0)
        {
            Requests.SendRequest("/container/v9", token, RestSharp.Method.GET);
            if (((int)Requests.Response.StatusCode) != 200)
                return MessageBox.Show($"{Requests.Response.StatusCode}");
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }
    }
}
