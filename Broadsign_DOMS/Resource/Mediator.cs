using Broadsign_DOMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Resource
{
    public class Mediator
    {
        static IDictionary<string, List<Action<object>>> tknList = new Dictionary<string, List<Action<object>>>();

        public static void Subscribe(string token, Action<object> callback)
        {
            if (!tknList.ContainsKey(token))
            {
                List<Action<object>> list = new List<Action<object>>();
                list.Add(callback);
                tknList.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach(var item in tknList[token])
                {
                    if(item.Method == callback.Method)
                        found = true;
                }
                if(!found)
                    tknList[token].Add(callback);
            }
        }
        public static void UnSubscribe(string token, Action<object> callback)
        {
            if(tknList.ContainsKey(token))
                tknList[token].Remove(callback);
        }

        public static void Notify(string token,string args = null)
        {
            if (tknList.ContainsKey(token))
            {
                foreach (var callback in tknList[token])
                {
                    callback(args);
                }
            }
        }
    }
}
