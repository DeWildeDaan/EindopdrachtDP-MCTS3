using Newtonsoft.Json;
using ProjectDaanDeWilde.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDaanDeWilde.Models
{
    public class ChargePoint: IDetailItem, IComparable
    {
        [JsonProperty(PropertyName = "ID")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "UsageTypeID")]
        public int UsageTypeId {get; set;}

        [JsonProperty(PropertyName = "StatusTypeID")]
        public int StatusTypeId { get; set; }

        [JsonProperty(PropertyName = "AddressInfo")]
        public Address AddressInfo { get; set; }

        [JsonProperty(PropertyName = "Connections")]
        public List<Connection> Connections { get; set; }

        public string UsageType { get {

                AllRefData refdata = RefDataRepository.RefData;

                return refdata.UsageTypes.FindAll(e => e.Id == this.UsageTypeId)[0].Title;
        } }

        public int ConnectionCount { 
            get {
                return this.Connections.Count;
            } }

        public string StatusType
        {
            get
            {

                AllRefData refdata = RefDataRepository.RefData;

                return refdata.StatusTypes.FindAll(e => e.Id == this.StatusTypeId)[0].Title;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            ChargePoint ander = (ChargePoint)obj;
            return this.AddressInfo.Distance.CompareTo(ander.AddressInfo.Distance);
        }
    }
}
