using Newtonsoft.Json;
using ProjectDaanDeWilde.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDaanDeWilde.Models
{
    public class Connection
    {
        [JsonProperty(PropertyName = "ConnectionTypeID")]
        public int ConnectionTypeId { get; set; }
        [JsonProperty(PropertyName = "StatusTypeID")]
        public int StatusTypeId { get; set; }
        [JsonProperty(PropertyName = "Quantity")]
        public int Quantity { get; set; }

        public string ConnectionType
        {
            get
            {

                AllRefData refdata = RefDataRepository.RefData;

                return refdata.ConnectionTypes.FindAll(e => e.Id == this.ConnectionTypeId)[0].Title;
            }
        }

        public string StatusType
        {
            get
            {

                AllRefData refdata = RefDataRepository.RefData;

                return refdata.StatusTypes.FindAll(e => e.Id == this.StatusTypeId)[0].Title;
            }
        }
    }
}
