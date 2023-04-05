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

        static public void SendRequest(string p, string t,Method method, object list = null)
        {
            client = new RestClient();
            client.Timeout = -1;
            client.BaseUrl = new Uri(r_Url + p);

            request = new RestRequest(method);   
            
            request.AddHeader("authorization", $"Bearer {t}");
            request.AddHeader("accept", $"application/json");
            request.AddHeader("content-type", $"application/json");

            if (list != null)
                request.AddJsonBody(list);

            response = client.Execute(request);
      
                      
        }

    }
}
