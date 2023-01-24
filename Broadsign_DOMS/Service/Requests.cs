using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using RestSharp;

namespace Broadsign_DOMS
{
    //change this to abstract class !
    abstract class Requests
    {

        #region fields
        static private string r_Request;
        static private string r_Url = "https://api.broadsign.com:10889/rest";
        static private RestClient client;
        static private RestRequest request;
        static private IRestResponse response;
        #endregion

        #region properties
        static public string Request { get => r_Request; set => r_Request = value; }
        static public IRestResponse Response { get => response; set => response = value; }
        #endregion

        static public void SendRequest(string p, string t,Method method, dynamic list = null)
        {
            client = new RestClient();
            client.Timeout = -1;
            client.BaseUrl = new Uri(r_Url + p);

            request = new RestRequest(method);            
            request.AddHeader("Authorization", $"Bearer {t}");
            if(list != null)
                request.AddJsonBody(list);

            response = client.Execute(request);
      
                      
        }

        //Considering deleting this part
        //static public void updateRequest(string req, dynamic list, string newToken)
        //{
        //    client = new RestClient();
        //    client.Timeout = -1;
        //    request = new RestRequest(Method.PUT);
        //    client.BaseUrl = new Uri(r_Url + req);
        //    request.AddHeader("Authorization", newToken);
        //    request.AddJsonBody(list);
        //    response = client.Execute(request);

        //}

    }
}
