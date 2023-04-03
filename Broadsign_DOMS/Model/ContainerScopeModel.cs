using Broadsign_DOMS.Resource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class ContainerScopeModel
    {
        private bool active;
        private bool can_see_above;
        private int domain_id;
        private int id;
        private int parent_id;
        private int scope_container_group_id;
        private int scope_container_id;
        private string scope_resource_type;

        public bool Active { get => active; set => active = value; }
        public bool Can_see_above { get => can_see_above; set => can_see_above = value; }
        public int Domain_id { get => domain_id; set => domain_id = value; }
        public int Id { get => id; set => id = value; }
        public int Parent_id { get => parent_id; set => parent_id = value; }
        public int Scope_container_group_id { get => scope_container_group_id; set => scope_container_group_id = value; }
        public int Scope_container_id { get => scope_container_id; set => scope_container_id = value; }
        public string Scope_resource_type { get => scope_resource_type; set => scope_resource_type = value; }

        private static dynamic _getContainerScopes(string token, int scope_id = 0)
        {
            Requests.SendRequest("/container_scope/v1", token, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }
        public static async Task GeneratContainerScopes(string t)
        {
            await Task.Delay(1);
            dynamic scopes = _getContainerScopes(t);

            if (scopes != null)
            {
                foreach (var container_scope in scopes["container_scope"])
                {
                    if (container_scope.active == true)
                    {
                        CommonResources.Container_Scopes.Add(new ContainerScopeModel
                        {
                            Active = container_scope.active,
                            Can_see_above = container_scope.can_see_above,
                            Domain_id = container_scope.domain_id,
                            Id = container_scope.id,
                            Parent_id = container_scope.parent_id,
                            Scope_container_group_id = container_scope.scope_container_group_id,
                            Scope_container_id = container_scope.scope_container_id,
                            Scope_resource_type = container_scope.scope_resource_type,
                        });
                    }
                }
            }
        }
    }
}
