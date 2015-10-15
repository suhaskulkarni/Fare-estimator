using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UberFareEstimateFramework.OAuthService
{
    public class OAuthUberProviderResult
    {
        public OAuthUberProviderResult(string productId, string currencyCode, string displayName, string priceEstimate, string lowPriceEstimate,
                                        string highPriceEstimate, string surgeMultiplier, string timeEstimate, string distance,
                                        HttpStatusCode code)
        {
            this.ProductId = productId;
            this.CurrencyCode = currencyCode;
            this.DisplayName = displayName;
            this.PriceEstimate = priceEstimate;
            this.LowPriceEstimate = lowPriceEstimate;
            this.HighPriceEstimate = highPriceEstimate;
            this.SurgeMultiplier = surgeMultiplier;
            this.TimeEstimate = timeEstimate;
            this.Distance = distance;
            this.StatusCode = code;
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

        public HttpStatusCode StatusCode { get; set; }
    }
}
