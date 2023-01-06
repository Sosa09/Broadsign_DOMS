using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace Broadsign_DOMS
{
    //change this to abstract class !
    abstract class Requests
    {

        
        static private string r_Request;
        static private string r_Url = "https://bssbeta.broadsign.com:10889/";
        static private RestClient client;
        static private RestRequest request;
        static private IRestResponse response;
        static private string token = "Bearer f433f376b376ff8f5e1157ab909ce8af";



        static public void getRequest(string req, string newToken)
        {
            client = new RestClient();
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            client.BaseUrl = new Uri(r_Url + req);
            request.AddHeader("Authorization", newToken);
            response = client.Execute(request);
                      
        }

        static public void updateRequest(string req, dynamic list, string newToken)
        {
            client = new RestClient();
            client.Timeout = -1;
            request = new RestRequest(Method.PUT);
            client.BaseUrl = new Uri(r_Url + req);
            request.AddHeader("Authorization", newToken);
            request.AddJsonBody(list);
            response = client.Execute(request);

        }

        static public string Request { get => r_Request; set => r_Request = value; }
        static public IRestResponse Response { get => response; set => response = value; }
    }
}
