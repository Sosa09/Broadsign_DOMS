using Broadsign_DOMS.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class ContainerScopeRelation : ObservableObject
    {
        private bool active;
        private int domain_id;
        private int id;
        private int parent_id;
        private int user_id;

        public bool Active { get => active; set => active = value; }
        public int Domain_id { get => domain_id; set => domain_id = value; }
        public int Id { get => id; set => id = value; }
        public int Parent_id { get => parent_id; set => parent_id = value; }
        public int User_id { get => user_id; set => user_id = value; }

        public static dynamic GetScopingRelation(string token)
        {
            string path = "/container_scope_relationship/v1";
            Requests.SendRequest(path, token, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }
    }
}
