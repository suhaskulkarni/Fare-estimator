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
            HttpResponseMessage priceEstimateResponseMessage = new HttpResponseMessage();
            HttpResponseMessage timeEstimateResponseMessage = new HttpResponseMessage();
            List<OAuthUberProviderResult> providerResult = new List<OAuthUberProviderResult>();
            try
            {
                string priceEstimateuri = string.Format(oauthService.RequestUri + "/" + oauthService.RequestPriceEstimate + "?" +
                            Constants.ServerTokenString + "=" + oauthService.ServerToken + "&" +
                            oauthService.SourceLatitudeString + "=" + oauthService.SourceLatitude + "&" +
                            oauthService.SourceLongitudeString + "=" + oauthService.SourceLongitude + "&" +
                            oauthService.DestinationLatitudeString + "=" + oauthService.DestinationLatitude + "&" +
                            oauthService.DestinationLongitudeString + "=" + oauthService.DestinationLongitude);

                priceEstimateResponseMessage = await httpClient.GetAsync(priceEstimateuri);

                string timeEstimateUri = string.Format(oauthService.RequestUri + "/" + oauthService.RequestTimeEstimate + "?" +
                                    Constants.ServerTokenString + "=" + oauthService.ServerToken + "&" +
                                    oauthService.SourceLatitudeString + "=" + oauthService.SourceLatitude + "&" +
                                    oauthService.SourceLongitudeString + "=" + oauthService.SourceLongitude);

                timeEstimateResponseMessage = await httpClient.GetAsync(timeEstimateUri);

                if (priceEstimateResponseMessage.StatusCode == HttpStatusCode.OK && timeEstimateResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    string priceEstimatetoken = await priceEstimateResponseMessage.Content.ReadAsStringAsync();
                    string timeEstimateToken = await timeEstimateResponseMessage.Content.ReadAsStringAsync();

                    JObject jsonTimeEstimateString = JObject.Parse(timeEstimateToken);
                    var timeList = jsonTimeEstimateString["times"];
                    List<UberData> timeEstimateProducts = JsonConvert.DeserializeObject<List<UberData>>(timeList.ToString());

                    JObject jsonPriceEstimateString = JObject.Parse(priceEstimatetoken);
                    var priceList = jsonPriceEstimateString["prices"];
                    List<UberData> priceEstimateProducts = JsonConvert.DeserializeObject<List<UberData>>(priceList.ToString());

                    foreach (var product in timeEstimateProducts)
                    {
                        string productId = product.product_id;
                        string displayName = product.display_name;
                        string timeEstimate = product.estimate;

                        foreach(var prod in priceEstimateProducts)
                        {
                            if (prod.product_id == product.product_id)
                            {
                                string currencyCode = prod.currency_code;
                                string priceEstimate = prod.estimate;
                                string lowEstimate = prod.low_estimate;
                                string highEstimate = prod.high_estimate;
                                string surge = prod.surge_multiplier;
                                string distance = prod.distance;
                                providerResult.Add(new OAuthUberProviderResult(productId, currencyCode, displayName, priceEstimate,
                                            lowEstimate, highEstimate, surge, timeEstimate, distance, priceEstimateResponseMessage.StatusCode));
                                break;
                            }

                        }
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
