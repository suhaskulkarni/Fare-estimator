using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberFareEstimateFramework.OAuthService
{
    public class OAuthOlaProviderResult
    {
        public OAuthOlaProviderResult(string category, string distance, string travelTime, string lowEstimate, string highEstimate)
        {
            this.DisplayName = category;
            this.Distance = distance;
            this.TimeEstimate = travelTime;
            this.LowPriceEstimate = lowEstimate;
            this.HighPriceEstimate = highEstimate;
        }

        public string ProductId { get; set; }

        public string CurrencyCode { get; set; }

        public string DisplayName { get; set; }

        public string PriceEstimate { get; set; }

        public string LowPriceEstimate { get; set; }

        public string HighPriceEstimate { get; set; }

        public string TimeEstimate { get; set; }

        public string SurgeMultiplier { get; set; }

        public string Distance { get; set; }
    }
}
