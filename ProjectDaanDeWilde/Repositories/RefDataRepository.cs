using Newtonsoft.Json;
using ProjectDaanDeWilde.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaanDeWilde.Repositories
{
    public static class RefDataRepository
    {
        //https://api.openchargemap.io/v3/referencedata&key=53fcd174-cbda-41d3-bd13-ebaf65925f88&output
        private const string _BASEURI = "https://api.openchargemap.io/v3/referencedata";
        private const string _APIKEY = "c768367a-9ffe-4919-8099-e2131fc115d2";
        public static AllRefData RefData = new AllRefData();

        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-API-Key", $"{_APIKEY}");
            client.Timeout = TimeSpan.FromMinutes(10);
            return client;
        }

        private static string AddKeyAndParams(string url)
        {
            return $"{url}?key={_APIKEY}&output";
        }

        public static async Task GetRefDataAsync()
        {
            using (HttpClient client = GetHttpClient())
            {
                string url = AddKeyAndParams(_BASEURI);
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        AllRefData data = JsonConvert.DeserializeObject<AllRefData>(json);
                        RefData = data;
                    }
                    RefData = null;
                }
                catch (Exception ex)
                {
                    throw ex;                    
                }
            }
        }

        public static void GetRefDataFile()
        {

            try
            {
                string json = "";

                var assembly = typeof(Address).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("ProjectDaanDeWilde.Assets.referencedata.txt");
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }

                if (json != null)
                {
                    AllRefData data = JsonConvert.DeserializeObject<AllRefData>(json);
                    RefData = data;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
