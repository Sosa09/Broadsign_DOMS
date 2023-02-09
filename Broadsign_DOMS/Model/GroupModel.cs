using Broadsign_DOMS.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class GroupModel : ObservableObject
    {
        private bool _active;
        private int _domain_id;
        private int _container_id;
        private int _id;
        private string _name;

        public bool Active { get => _active; set => _active = value; }
        public int Domain_id { get => _domain_id; set => _domain_id = value; }
        public int Container_id { get => _container_id; set => _container_id = value; }
        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Domain_name { get; set; }


        public static dynamic GetGroups(string token, int user_id = 0, int domain_id = 0, int usergroup_id = 0)
        {
            string path = $"/group/v4";
            Requests.SendRequest(path, token, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        } 
    }
}
