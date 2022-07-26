using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace skateclub.Utility
{
    public static class Network
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<HttpResponseMessage> Post(string url, Dictionary<string, string> values)
        {
            try
            {
                var content = new FormUrlEncodedContent(values);

                return await client.PostAsync(url, content);
            } 
            catch
            {
                return null;
            }
        }

        public static async Task<HttpResponseMessage> Get(string url)
        {
            try
            {
                return await client.GetAsync(url);
            } 
            catch
            {
                return null;
            }
        }
    }
}
