using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDaanDeWilde.Models
{
    public class AllRefData
    {
        [JsonProperty(PropertyName = "UsageTypes")]
        public List<UsageType> UsageTypes { get; set; }
        [JsonProperty(PropertyName = "ConnectionTypes")]
        public List<ConnectionType> ConnectionTypes { get; set; }
        [JsonProperty(PropertyName = "StatusTypes")]
        public List<StatusType> StatusTypes { get; set; }
    }
}
