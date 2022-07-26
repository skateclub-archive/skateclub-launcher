using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using skateclub.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BCrypt.Net;
using System.Net;
using System.Runtime.InteropServices;

namespace skateclub.Core.Server
{
    public struct ServerInfo
    {
        public string id;
        public string name;
        public string description;
        public int players;
        public int capacity;

        public string country;
        public string flag;

        public bool hasPassword;
        public bool official;
    }
    public struct ServerListing
    {
        public string name;
        public string description;
        public string level;

        public string password;
        public string[] banlist;
        public string[] allowlist;

        public int playerCount;
        public int playerCapacity;
    }

    public static class ServerList
    {
        static string CommKey = "1337";

        public static string ServerListAddress = "REMOVED";
        public static string EncryptionKey { get; private set; } = "REMOVED";

        public delegate void ServerListingUpdate(bool success, string message);
  //      public delegate ServerListing ServerListingBeforeUpdate(ServerListing current);

    //    public static event ServerListingBeforeUpdate OnServerListingBeforeUpdate;
        public static event ServerListingUpdate OnServerListingUpdate;

        public static ServerListing serverListing { get; private set; }


        public static int refreshFrequency { get; private set; } = 5;

        public static async Task<ServerInfo[]> GetList()
        { 
            var request = await Network.Post(ServerListAddress + "/list", new Dictionary<string, string>()
            {
                { "serverKey", CommKey }
            });

            if (request != null && request.IsSuccessStatusCode)
            {
                var json = (JObject)JsonConvert.DeserializeObject(await request.Content.ReadAsStringAsync());

                refreshFrequency = (int)json["updateFrequency"];

                var array = JsonConvert.DeserializeObject<ServerInfo[]>(json["servers"].ToString());
                return array;
            }
            else
            {
                return null;
            }
        }

        public static async Task<HttpResponseMessage> ReportServer(string id, string discord, string reason)
        {
            return await Network.Post(ServerListAddress + "/report", new Dictionary<string, string>()
            {
                { "serverKey", CommKey },
                { "id", id },
                { "discord", discord },
                { "reason", reason }
            });
        }

        public static async Task<HttpResponseMessage> ConnectToServer(string id, string password)
        {
            return await Network.Post(ServerListAddress + "/connect", new Dictionary<string, string>()
            {
                { "serverKey", CommKey },
                { "id", id },
                {"password", password }
            });
        }

        public static async void RegisterServerListing(ServerListing listing)
        {
            //   var listing = OnServerListingBeforeUpdate?.Invoke(serverListing);

            //   if (listing == null) listing = serverListing;

            serverListing = listing;

            var values = new Dictionary<string, string>()
            {
                { "serverKey", CommKey },
                { "serverName", listing.name },
                { "serverDescription", listing.description },

                { "serverPort", "25200" },

                { "serverPlayers", listing.playerCount.ToString() },
                { "serverCapacity", listing.playerCapacity.ToString() },

                { "level", listing.level },

                { "password", listing.password },

                { "banlist", listing.banlist.Length > 0 ? JsonConvert.SerializeObject(listing.banlist) : "" },
                { "allowlist", listing.allowlist.Length > 0 ? JsonConvert.SerializeObject(listing.allowlist) : "" }
            };

            var request = await Network.Post(ServerListAddress + "/register", values);

            if (request != null)
            {
                string response = await request.Content.ReadAsStringAsync();

                string message = string.IsNullOrEmpty(response) ? request.StatusCode.ToString() : response;

                if (!request.IsSuccessStatusCode)
                {
                    try
                    {
                        var json = JsonConvert.DeserializeObject<JToken>(response);

                        if (json != null && json.Contains("error"))
                            message = (string)json["error"];
                    }
                    catch { }
                }

                OnServerListingUpdate?.Invoke(request.IsSuccessStatusCode, message);
            }
        }

        public static async void RemoveServerListing()
        {
            await Network.Post(ServerListAddress + "/remove", new Dictionary<string, string>()
            {
                { "serverKey", CommKey }
            });
        }
    }
}
