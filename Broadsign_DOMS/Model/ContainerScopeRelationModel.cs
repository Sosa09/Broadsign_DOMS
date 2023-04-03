using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class ContainerScopeRelationModel : ObservableObject
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

        private static dynamic _getScopingRelation(string token)
        {
            string path = "/container_scope_relationship/v1";
            Requests.SendRequest(path, token, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }

        public static async Task GenerateScopingRelations(string t)
        {
            await Task.Delay(1);
            dynamic relation_users_containers = _getScopingRelation(t);

            if (relation_users_containers != null)
            {
                foreach (var ugsRelation in relation_users_containers["container_scope_relationship"])
                {
                    if (ugsRelation.active == true)
                    {
                        CommonResources.Container_Scope_Relations.Add(new ContainerScopeRelationModel
                        {
                            Active = ugsRelation.active,
                            Domain_id = ugsRelation.domain_id,
                            Id = ugsRelation.id,
                            Parent_id = ugsRelation.parent_id,
                            User_id = ugsRelation.user_id
                        });
                    }
                }

            }
        }
    }
}
