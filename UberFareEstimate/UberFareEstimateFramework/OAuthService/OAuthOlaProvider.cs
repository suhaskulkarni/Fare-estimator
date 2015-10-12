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
    public class OAuthOlaProvider
    {
        public async Task<List<OAuthOlaProviderResult>> GetFareEstimates(OAuthOlaService oauthService)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add(Constants.AppToken, oauthService.ServerToken);

            string urlMini = String.Format(oauthService.RequestUri + "/" + oauthService.RequestPriceEstimate + "?" + 
                            oauthService.SourceLatitudeString + "=" + oauthService.SourceLatitude + "&" + 
                            oauthService.SourceLongitudeString + "=" + oauthService.SourceLongitude + "&" +
                            oauthService.DestinationLatitudeString + "=" + oauthService.DestinationLatitude + "&" + 
                            oauthService.DestinationLongitudeString + "=" + oauthService.DestinationLongitude + "&" +
                            oauthService.VehicleCategoryString + "=" + oauthService.VehicleCategoryMini);
            HttpResponseMessage httpResponseMessageMini = await httpClient.GetAsync(urlMini);
            string tokenMini = await httpResponseMessageMini.Content.ReadAsStringAsync();
            dynamic jsonStringMini = JsonConvert.DeserializeObject(tokenMini);

            string urlSedan = String.Format(oauthService.RequestUri + "/" + oauthService.RequestPriceEstimate + "?" +
                oauthService.SourceLatitudeString + "=" + oauthService.SourceLatitude + "&" +
                oauthService.SourceLongitudeString + "=" + oauthService.SourceLongitude + "&" +
                oauthService.DestinationLatitudeString + "=" + oauthService.DestinationLatitude + "&" +
                oauthService.DestinationLongitudeString + "=" + oauthService.DestinationLongitude + "&" +
                oauthService.VehicleCategoryString + "=" + oauthService.VehicleCategorySedan);
            HttpResponseMessage httpResponseMessageSedan = await httpClient.GetAsync(urlSedan);
            string tokenSedan = await httpResponseMessageSedan.Content.ReadAsStringAsync();
            dynamic jsonStringSedan = JsonConvert.DeserializeObject(tokenSedan);

            string urlPrime = String.Format(oauthService.RequestUri + "/" + oauthService.RequestPriceEstimate + "?" +
                oauthService.SourceLatitudeString + "=" + oauthService.SourceLatitude + "&" +
                oauthService.SourceLongitudeString + "=" + oauthService.SourceLongitude + "&" +
                oauthService.DestinationLatitudeString + "=" + oauthService.DestinationLatitude + "&" +
                oauthService.DestinationLongitudeString + "=" + oauthService.DestinationLongitude + "&" +
                oauthService.VehicleCategoryString + "=" + oauthService.VehicleCategoryPrime);
            HttpResponseMessage httpResponseMessagePrime = await httpClient.GetAsync(urlPrime);
            string tokenPrime = await httpResponseMessagePrime.Content.ReadAsStringAsync();
            dynamic jsonStringPrime = JsonConvert.DeserializeObject(tokenPrime);

            List<OAuthOlaProviderResult> providerResult = new List<OAuthOlaProviderResult>();

            providerResult.Add(new OAuthOlaProviderResult(jsonStringMini.ride_estimate[0].category, jsonStringMini.ride_estimate[0].distance, 
                                jsonStringMini.ride_estimate[0].travel_time_in_minutes, jsonStringMini.ride_estimate[0].amount_min, 
                                jsonStringMini.ride_estimate[0].amount_max));

            providerResult.Add(new OAuthOlaProviderResult(jsonStringSedan.ride_estimate[0].category, jsonStringSedan.ride_estimate[0].distance,
                    jsonStringSedan.ride_estimate[0].travel_time_in_minutes, jsonStringSedan.ride_estimate[0].amount_min,
                    jsonStringSedan.ride_estimate[0].amount_max));

            providerResult.Add(new OAuthOlaProviderResult(jsonStringPrime.ride_estimate[0].category, jsonStringPrime.ride_estimate[0].distance,
                    jsonStringPrime.ride_estimate[0].travel_time_in_minutes, jsonStringPrime.ride_estimate[0].amount_min,
                    jsonStringPrime.ride_estimate[0].amount_max));

            return providerResult;
        }
    }
}
