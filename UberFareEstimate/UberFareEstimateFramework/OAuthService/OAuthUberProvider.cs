using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UberFareEstimateFramework.Common;

namespace UberFareEstimateFramework.OAuthService
{
    public class OAuthUberProvider
    {
        public async Task<List<OAuthUberProviderResult>> GetFareEstimates(OAuthUberService oauthService)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            List<OAuthUberProviderResult> providerResult = new List<OAuthUberProviderResult>();
            try
            {
                string uri = string.Format(oauthService.RequestUri + "/" + oauthService.RequestPriceEstimate + "?" +
                            Constants.ServerTokenString + "=" + oauthService.ServerToken + "&" +
                            oauthService.SourceLatitudeString + "=" + oauthService.SourceLatitude + "&" +
                            oauthService.SourceLongitudeString + "=" + oauthService.SourceLongitude + "&" +
                            oauthService.DestinationLatitudeString + "=" + oauthService.DestinationLatitude + "&" +
                            oauthService.DestinationLongitudeString + "=" + oauthService.DestinationLongitude);

                responseMessage = await httpClient.GetAsync(uri);
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    string token = await responseMessage.Content.ReadAsStringAsync();
                    //dynamic jsonString = JsonConvert.DeserializeObject(token);

                    JObject jsonString = JObject.Parse(token);
                    var priceList = jsonString["prices"];
                    List<UberData> products = JsonConvert.DeserializeObject<List<UberData>>(priceList.ToString());

                    foreach (var product in products)
                    {
                        string productId = product.product_id;
                        string currencyCode = product.currency_code;
                        string displayName = product.display_name;
                        string priceEstimate = product.estimate;
                        string lowEstimate = product.low_estimate;
                        string highEstimate = product.high_estimate;
                        string surge = product.surge_multiplier;
                        string duration = product.duration;
                        string distance = product.distance;
                        providerResult.Add(new OAuthUberProviderResult(productId, currencyCode, displayName, priceEstimate,
                                            lowEstimate, highEstimate, surge, duration, distance, responseMessage.StatusCode));
                    }
                    return providerResult;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }

    public class UberData
    {
        [JsonProperty("product_id")]
        public string product_id { get; set; }

        [JsonProperty("currency_code")]
        public string currency_code { get; set; }

        [JsonProperty("display_name")]
        public string display_name { get; set; }

        [JsonProperty("estimate")]
        public string estimate { get; set; }

        [JsonProperty("low_estimate")]
        public string low_estimate { get; set; }

        [JsonProperty("high_estimate")]
        public string high_estimate { get; set; }

        [JsonProperty("surge_multiplier")]
        public string surge_multiplier { get; set; }

        [JsonProperty("duration")]
        public string duration { get; set; }

        [JsonProperty("distance")]
        public string distance { get; set; }
    }
}
