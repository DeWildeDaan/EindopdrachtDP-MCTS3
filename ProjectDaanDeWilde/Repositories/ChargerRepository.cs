using Newtonsoft.Json;
using ProjectDaanDeWilde.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaanDeWilde.Repositories
{
    public static class ChargerRepository
    {
        //https://api.openchargemap.io/v3/poi/?key=f82d2c24-a826-4b11-9a86-21bca29012af&output=json&countrycode=BE&latitude=51.070340&longitude=4.084952&distance=10&distanceunit=KM&compact=true&verbose=false
        private const string _BASEURI = "https://api.openchargemap.io/v3/poi/";
        private const string _APIKEY = "798601c5-7bb5-442c-9d08-b1f5f3235a4a";

        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "ChargeMap/1.1 (studentproject.mct.be/daan.de.wilde@student.howest.be)");
            client.DefaultRequestHeaders.Add("X-API-Key", $"{_APIKEY}");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.Timeout = TimeSpan.FromMinutes(10);
            return client;
        }

        private static string AddKeyAndParams(string url, string lat, string longi, double distance)
        {
            lat = lat.Replace(',', '.');
            longi = longi.Replace(',', '.');
            return $"{url}?output=json&countrycode=BE&latitude={lat}&longitude={longi}&distance={distance}&distanceunit=KM&compact=true&verbose=false&client=chargemap1.0";
        }

        public static async Task<List<ChargePoint>> GetChargePointsAsync(double lat, double longi, double distance)
        {
            using (HttpClient client = GetHttpClient())
            {
                string url = AddKeyAndParams(_BASEURI, lat.ToString(), longi.ToString(), distance);
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        return JsonConvert.DeserializeObject<List<ChargePoint>>(json);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }

        public static List<ChargePoint> GetChargePointsFile(double distance)
        {

            try
            {
                string json = "";
                List<ChargePoint> filteredChargepoints = new List<ChargePoint>();

                var assembly = typeof(Address).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("ProjectDaanDeWilde.Assets.apidata.txt");
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }

                if (json != null)
                {
                    List<ChargePoint> unfilteredChargepoints = JsonConvert.DeserializeObject<List<ChargePoint>>(json);
                    foreach (var item in unfilteredChargepoints)
                    {
                        if (item.AddressInfo.Distance < distance)
                        {
                            filteredChargepoints.Add(item);
                        }
                    }
                    return filteredChargepoints;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
