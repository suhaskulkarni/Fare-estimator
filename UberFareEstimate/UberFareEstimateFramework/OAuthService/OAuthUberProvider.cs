using Newtonsoft.Json;
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
                    dynamic jsonString = JsonConvert.DeserializeObject(token);

                    foreach (var product in jsonString.prices)
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
}
