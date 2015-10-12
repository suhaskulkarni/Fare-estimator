using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

            string uri = string.Format(oauthService.RequestUri + "/" + oauthService.RequestPriceEstimate + "?" + 
                        Constants.ServerTokenString + "=" + oauthService.ServerToken + "&" + 
                        oauthService.SourceLatitudeString + "=" + oauthService.SourceLatitude + "&" + 
                        oauthService.SourceLongitudeString + "=" + oauthService.SourceLongitude + "&" + 
                        oauthService.DestinationLatitudeString + "=" + oauthService.DestinationLatitude + "&" +
                        oauthService.DestinationLongitudeString + "=" + oauthService.DestinationLongitude);

            HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);
            string token = await responseMessage.Content.ReadAsStringAsync();
            dynamic jsonString = JsonConvert.DeserializeObject(token);

            List<OAuthUberProviderResult> providerResult = new List<OAuthUberProviderResult>();

            foreach(var product in jsonString.prices)
            {
                providerResult.Add(new OAuthUberProviderResult(product.product_id, product.currency_code, product.display_name, product.estimate,
                                    product.low_estimate, product.high_estimate, product.surge_multiplier, product.duration, product.distance));
            }

            return providerResult;
        }
    }
}
