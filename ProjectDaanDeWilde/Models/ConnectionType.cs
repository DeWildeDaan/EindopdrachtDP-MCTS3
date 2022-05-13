using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDaanDeWilde.Models
{
    public class ConnectionType : IRefData
    {
        [JsonProperty(PropertyName = "ID")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }
    }
}
