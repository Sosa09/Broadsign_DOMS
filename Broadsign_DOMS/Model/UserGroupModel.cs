using Broadsign_DOMS.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class UserGroupModel : ObservableObject
    {
        private bool _active;
        private int _domain_id;
        private int _group_id;
        private int _id;
        private int _parent_id;

        public bool Active { get => _active; set => _active = value; }
        public int Domain_id { get => _domain_id; set => _domain_id = value; }
        public int Group_id { get => _group_id; set => _group_id = value; }
        public int Id { get => _id; set => _id = value; }
        public int Parent_id { get => _parent_id; set => _parent_id = value; }

        public static dynamic GetUserGroups(string token, int user_id = 0, int domain_id = 0, int usergroup_id = 0)
        {
            string path = $"/user_group/v4?parent_id={user_id}";
            Requests.SendRequest(path, token, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        } 
    }
}
