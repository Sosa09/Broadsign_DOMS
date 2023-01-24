using Broadsign_DOMS.Service;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;

namespace Broadsign_DOMS.Model
{
    public class UserModel : ObservableObject
    {
        #region variables
        private bool    active;
        private bool    allow_auth_token;
        private int     container_id;
        private int     domain_id;
        private string  email;
        private object  has_auth_token;
        private int     id;
        private string  name;
        private string  passwd;
        private string  pending_single_sign_on_email;
        private string  public_key_fingerprint;
        private int     single_sign_on_id;
        private string  username;
        private string  domain_name;
    
        #endregion
        #region Properties
        public bool Active { get => active; set => active = value; }
        public bool Allow_auth_token { get => allow_auth_token; set => allow_auth_token = value; }
        public int Container_id { get => container_id; set => container_id = value; }
        public int Domain_id { get => domain_id; set => domain_id = value; }
        public string Email { get => email; set => email = value; }
        public object Has_auth_token { get => has_auth_token; set => has_auth_token = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Passwd { get => passwd; set => passwd = value; }
        public string Pending_single_sign_on_email { get => pending_single_sign_on_email; set => pending_single_sign_on_email = value; }
        public string Public_key_fingerprint { get => public_key_fingerprint; set => public_key_fingerprint = value; }
        public int Single_sign_on_id { get => single_sign_on_id; set => single_sign_on_id = value; }
        public string Username { get => username; set => username = value; }
        public string Domain_name { get => domain_name; set => domain_name = value; }
    
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
