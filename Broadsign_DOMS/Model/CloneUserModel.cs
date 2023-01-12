using Broadsign_DOMS.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class CloneUserModel : ObservableObject
    {
        private string _username;
        private int _id;
        private List<int> _group_ids;
        private List<int> _scopingRelation_ids;
        private string name;
        private int userContainer_id;

        public string Username { get => _username; set => _username = value; }
        public int Id { get => _id; set => _id = value; }
        public List<int> Group_ids { get => _group_ids; set => _group_ids = value; }
        public List<int> ScopingRelation_ids { get => _scopingRelation_ids; set => _scopingRelation_ids = value; }
        public string Name { get => name; set => name = value; }
        public int UserContainer_id { get => userContainer_id; set => userContainer_id = value; }
    }
}
