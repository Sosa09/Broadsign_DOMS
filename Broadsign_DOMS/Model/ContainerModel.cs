using Broadsign_DOMS.Resource;
using Newtonsoft.Json;
using System.Threading.Tasks;
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


        private static dynamic _getContainers(string token, int id = 0)
        {
            Requests.SendRequest("/container/v9", token, RestSharp.Method.GET);
            if (((int)Requests.Response.StatusCode) != 200)
                return MessageBox.Show($"{Requests.Response.StatusCode}");
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }

        public static async Task GenerateContainers(string t)
        {
            await Task.Delay(1);

            dynamic containers = ContainerModel._getContainers(t);
            if (containers != null)
            {
                foreach (var container in containers["container"])
                {
                    //show message loading resource for country ...
                    if (container.active == true)
                    {
                        CommonResources.Containers.Add(new ContainerModel
                        {
                            Active = container.active,
                            Container_id = container.container_id,
                            Domain_id = container.domain_id,
                            Group_id = container.group_id,
                            Id = container.id,
                            Name = container.name,
                            Parent_id = container.parent_id,
                            Parent_resource_type = container.parent_resource_type

                        });
                    }
                }
            }
        }
    }
}
