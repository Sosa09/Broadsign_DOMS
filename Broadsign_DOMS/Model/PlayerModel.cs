using Broadsign_DOMS.Resource;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Broadsign_DOMS.Model
{
    public class PlayerModel : BroadsignAPIModel
    {
        
        public int Config_profile_bag_id { get; set; }
        
        public string Custom_unique_id { get; set; }
        public string Db_pickup_tm_utc { get; set; }
        public int Discovery_status { get; set; }
        public int Display_unit_id { get; set; }
        public int Domain_id { get; set; }
        public string Geolocation { get; set; }
        public string NewName { get; set; }

        public int Nscreens { get; set; }
        public string Primary_mac_address { get; set; }
        public string Public_key_fingerprint { get; set; }
        public string Remote_clear_db_tm_utc { get; set; }
        public string Remote_reboot_tm_utc { get; set; }
        public string Secondary_mac_address { get; set; }
        public int Volume { get; set; }
     
        public static dynamic GetPlayers(string token, int id = 0)
        {
            string path = "/host/v17";
            Requests.SendRequest(path, token, Method.GET);
            return JsonConvert.DeserializeObject(Requests.Response.Content);
        }
        public static void UpdatePlayers(string token, PlayerModel player = null, ObservableCollection<object> players = null)
        {
            if(player == null && players == null)
            {
                MessageBox.Show("No players select or found");
                return;
            }
            string path = "/host/v17/";
            foreach (PlayerModel p in players)
            {
                dynamic requestBody = @"{" +
                                    "\"config_profile_bag_id\":" + p.Config_profile_bag_id + "," +
                                    "\"container_id\": \"" + p.Container_id + "," +
                                    "\"db_pickup_tm_utc\": \"" + p.Db_pickup_tm_utc + "," +
                                    "\"discovery_status\":" + p.Discovery_status + "," +
                                    "\"display_unit_id\":" + p.Display_unit_id + "," +
                                    "\"domain_id\":" + p.Domain_id + "," +
                                    "\"geolocation\":" + p.Geolocation + "," +
                                    "\"id\":" + p.Id + "," +
                                    "\"name\":" + p.NewName + "," +
                                    "\"nscreens\":" + p.Nscreens + "," +
                                    "\"public_key_fingerprint\":" + p.Public_key_fingerprint + "," +
                                    "\"remote_clear_db_tm_utc\":" + p.Remote_clear_db_tm_utc + "," +
                                    "\"remote_reboot_tm_utc\":" + p.Remote_reboot_tm_utc + "," +
                                    "\"volume\":" + p.Volume +
                                    "\"}";
                Requests.SendRequest(path, token, Method.POST, requestBody);
                MessageBox.Show(Requests.Response.ResponseStatus.ToString());
                p.Name = p.NewName;
                p.NewName = "";
            }
            


        
            

        }

        public static async Task GeneratePlayers(string token)
        {
            await Task.Delay(1);
            try
            {
                dynamic players = GetPlayers(token);
                if (players != null)
                {
                    foreach (dynamic player in players.host)
                    {
                        if (player.active == true)
                        {
                            CommonResources.Players.Add(new PlayerModel
                            {
                                Active = player.active,
                                Config_profile_bag_id = player.config_profile_bag_id,
                                Container_id = player.container_id,
                                Custom_unique_id = player.ustom_unique_id,
                                Db_pickup_tm_utc = player.db_pickup_tm_utc,
                                Discovery_status = player.discovery_status,
                                Display_unit_id = player.display_unit_id,
                                Domain_id = player.domain_id,
                                Geolocation = player.geolocation,
                                Id = player.id,
                                Name = player.name,
                                Nscreens = player.nscreens,
                                Primary_mac_address = player.primary_mac_address,
                                Public_key_fingerprint = player.public_key_fingerprint,
                                Remote_clear_db_tm_utc = player.remote_clear_db_tm_utc,
                                Remote_reboot_tm_utc = player.remote_reboot_tm_utc,
                                Secondary_mac_address = player.secondary_mac_address,
                                Volume = player.volume
                            });
                        }

                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
