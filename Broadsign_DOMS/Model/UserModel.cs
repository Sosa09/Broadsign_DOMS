using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private bool    has_auth_token;
        private int     id;
        private string  name;
        private string  passwd;
        private string  pending_single_sign_on_email;
        private string  public_key_fingerprint;
        private int     single_sign_on_id;
        private string  username;
        #endregion
        #region Properties
        public bool Active { get => active; set => active = value; }
        public bool Allow_auth_token { get => allow_auth_token; set => allow_auth_token = value; }
        public int Container_id { get => container_id; set => container_id = value; }
        public int Domain_id { get => domain_id; set => domain_id = value; }
        public string Email { get => email; set => email = value; }
        public bool Has_auth_token { get => has_auth_token; set => has_auth_token = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Passwd { get => passwd; set => passwd = value; }
        public string Pending_single_sign_on_email { get => pending_single_sign_on_email; set => pending_single_sign_on_email = value; }
        public string Public_key_fingerprint { get => public_key_fingerprint; set => public_key_fingerprint = value; }
        public int Single_sign_on_id { get => single_sign_on_id; set => single_sign_on_id = value; }
        public string Username { get => username; set => username = value; }
        #endregion

        #region Get Method request Function
        public void getUser(int id = 0)
        {
            if(id == 0)
        }
        #endregion

    }
}
