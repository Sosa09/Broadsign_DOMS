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
    public class ContainerScopeRelationModel : BroadsignAPIModel
    {

        public int User_id { get; set; }

        private static dynamic _getScopingRelation(string token)
        {
            string path = "/container_scope_relationship/v1";
            Requests.SendRequest(path, token, RestSharp.Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }

        public static async Task GenerateScopingRelations(Domain domain)
        {
            await Task.Delay(1);
            dynamic relation_users_containers = _getScopingRelation(domain.Token);

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
                            User_id = ugsRelation.user_id,
                            AssignedDomain = domain
                        });
                    }
                }

            }
        }
    }
}
