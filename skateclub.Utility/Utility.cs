using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace skateclub.Utility
{
    public static class Utility
    {
        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public static async Task<string> GetIPAddress()
        {
            string address = "";

            while (!IPAddress.TryParse(address, out var ip))
            {
                var req = await Network.Get("http://checkip.dyndns.org/");
                using (StreamReader stream = new StreamReader(await req.Content.ReadAsStreamAsync()))
                {
                    address = stream.ReadToEnd();
                }

                int first = address.IndexOf("Address: ") + 9;
                int last = address.LastIndexOf("</body>");
                address = address.Substring(first, last - first);
            }

            return address;
        }


        public static async Task<string> GetCountry(string ip)
        {
            string country = "";

            if (Uri.TryCreate(ip, UriKind.RelativeOrAbsolute, out var uri))
                ip = Dns.GetHostAddresses(ip)[0].ToString();

            var req = await Network.Get("http://ip-api.com/json/" + ip);

            if (req != null && req.IsSuccessStatusCode)
            {
                country = (string)JsonConvert.DeserializeObject<JToken>(await req.Content.ReadAsStringAsync())["country"];
            }

            return country;
        }

        public static async Task<bool> CheckForConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                if (url == null)
                    url = "http://www.gstatic.com/generate_204";

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;

                var response = await request.GetResponseAsync();

                if (response != null)
                    return true;
            }
            catch { }

            return false;
        }

        public static string[] SplitString(this string ins, string[] args)
        {
            string[] lines = ins.Split(
                args,
                StringSplitOptions.None
                );

            return lines;
        }
    }
}
