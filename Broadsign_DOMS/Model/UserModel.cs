using Broadsign_DOMS.Service;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;

namespace Broadsign_DOMS.Model
{
    public class UserModel : BroadsignAPIModel
    {

   
        #region Properties
        
        public bool Allow_auth_token { get; set; }
        
        public int Domain_id { get; set; }
        public string Email { get; set; }
        public object Has_auth_token { get; set; }
        
       
        public string Passwd { get; set; }
        public string Pending_single_sign_on_email { get; set; }
        public string Public_key_fingerprint { get; set; }
        public int Single_sign_on_id { get; set; }
        public string Username { get; set; }
        public string Domain_name { get; set; }
    
        #endregion

        #region Get Method request Function
        public static dynamic GetUsers(string token,int id = 0)
        {
            string path = "/user/v13";
            if (id != 0)
                path += $"by_id?ids={id}";
            
            Requests.SendRequest(path, token, Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);

        }

        public static void AddUsers(string token,UserModel user = null, List<UserModel> users = null)
        {
            if(user == null && users == null)
            {
                MessageBox.Show("Please add at least one user as argument");
                return;
            };
            string path = "/user/v13/add";
            dynamic requestBody = @"{ " +
                "\"container_id\":" + user.Container_id + ", " +
                "\"domain_id\":" + user.Domain_id + ", " +
                "\"name\": \"" + user.Name + "\", " +
                "\"passwd\": \"\", " +
                "\"sub_elements\": { " +
                    "\"container_scope\": [ " +
                        "{ \"can_see_above\": true, " +
                        "\"scope_container_group_id\": 0, " +
                        "\"scope_container_id\": 0 } ], " +
                "\"group\": " +
                    "[ { \"id\":0  } ] }, " +
                "\"username\": \"" + user.Username + "\"}";

            Requests.SendRequest(path, token, Method.POST, requestBody);
            MessageBox.Show(Requests.Response.ResponseStatus.ToString());
        }
        #endregion

    }
}
