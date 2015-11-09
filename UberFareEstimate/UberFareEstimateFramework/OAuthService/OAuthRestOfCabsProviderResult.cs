using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberFareEstimateFramework.OAuthService
{
    public class OAuthRestOfCabsProviderResult
    {
        public OAuthRestOfCabsProviderResult(string cabServiceProvider, string category, string distance, string travelTime, string priceEstimate)
        {
            this.cabServiceProvider = cabServiceProvider;
            this.DisplayName = category;
            this.Distance = distance;
            this.TimeEstimate = travelTime;
            this.PriceEstimate = priceEstimate;
        }

        public string cabServiceProvider { get; set; }

        public string ProductId { get; set; }

        public string CurrencyCode { get; set; }

        public string DisplayName { get; set; }

        public string PriceEstimate { get; set; }

        public string TimeEstimate { get; set; }

        public string SurgeMultiplier { get; set; }

        public string Distance { get; set; }
    }
}
