using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberFareEstimateFramework.Json
{
    public class CabServices
    {
        [JsonProperty("cabServiceDisplayName")]
        public string CabServiceDisplayName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("baseFare")]
        public string BaseFare { get; set; }

        [JsonProperty("baseDistance")]
        public string BaseDistance { get; set; }

        [JsonProperty("perMinCharge")]
        public string PerMinCharge { get; set; }

        [JsonProperty("ratePerKm")]
        public string RatePerKm { get; set; }

        [JsonProperty("waitingCharges")]
        public string WaitingCharges { get; set; }

        [JsonProperty("surgeMultiplier")]
        public string SurgeMultiplier { get; set; }
    }
}
