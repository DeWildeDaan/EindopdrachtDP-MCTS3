using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDaanDeWilde.Models
{
    public class Address
    {
        [JsonProperty(PropertyName = "Title")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "AddressLine1")]
        public string AddressLine { get; set; }
        [JsonProperty(PropertyName = "Town")]
        public string Town { get; set; }
        [JsonProperty(PropertyName = "Postcode")]
        public string PostCode { get; set; }
        [JsonProperty(PropertyName = "Latitude")]
        public double Lat { get; set; }
        [JsonProperty(PropertyName = "Longitude")]
        public double Long { get; set; }
        [JsonProperty(PropertyName = "Distance")]
        public double Distance { get; set; }
    }
}
