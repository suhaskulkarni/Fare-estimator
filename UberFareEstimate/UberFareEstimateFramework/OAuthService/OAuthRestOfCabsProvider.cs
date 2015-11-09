using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UberFareEstimateFramework.Json;

namespace UberFareEstimateFramework.OAuthService
{
    public class OAuthRestOfCabsProvider
    {
        public async Task<List<OAuthRestOfCabsProviderResult>> GetFareEstimates(OAuthRestOfCabsService oauthService)
        {
            HttpClient httpClient = new HttpClient();
            List<OAuthRestOfCabsProviderResult> providerResult = new List<OAuthRestOfCabsProviderResult>();
            try
            {
                string timeUrl = String.Format(oauthService.RequestUri + "/" + oauthService.RequestTimeEstimate + "?" +
                             oauthService.SourceLatitudeString + "=" + oauthService.SourceLatitude + "&" +
                             oauthService.SourceLongitudeString + "=" + oauthService.SourceLongitude);

                HttpResponseMessage timeResponseMessage = await httpClient.GetAsync(new Uri(timeUrl));
                if (timeResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string timeToken = await timeResponseMessage.Content.ReadAsStringAsync();
                    JObject jsonTimeEstimateString = JObject.Parse(timeToken);
                    var timeList = jsonTimeEstimateString["data"];

                    List<EstimateTimeClass> timeEstimateProducts = JsonConvert.DeserializeObject<List<EstimateTimeClass>>(timeList.ToString());
                    List<CabServices> requestBodyProducts = JsonConvert.DeserializeObject<List<CabServices>>(timeList.ToString());

                    string priceUrl = string.Format(oauthService.RequestUri + "/" + oauthService.RequestPriceEstimate + "?" +
                                        oauthService.SourceLatitudeString + "=" + oauthService.SourceLatitude + "&" +
                                        oauthService.SourceLongitudeString + "=" + oauthService.SourceLongitude + "&" +
                                        oauthService.DestinationLatitudeString + "=" + oauthService.DestinationLatitude + "&" +
                                        oauthService.DestinationLongitudeString + "=" + oauthService.DestinationLongitude);

                    string jsonRequestBody = JsonConvert.SerializeObject(new { cabs = requestBodyProducts, city = oauthService.CityOfBooking },
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});

                    var content = new FormUrlEncodedContent(new[] 
                    {
                        new KeyValuePair<string, string>("cabs", jsonRequestBody)
                    });
                    HttpResponseMessage priceResponseMessage = await httpClient.PostAsync(new Uri(priceUrl), content);
                    if (priceResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string priceToken = await priceResponseMessage.Content.ReadAsStringAsync();
                        JObject jsonPriceEstimateString = JObject.Parse(priceToken);
                        var priceList = jsonPriceEstimateString["data"];

                        List<EstimatePriceClass> priceEstimateProducts = JsonConvert.DeserializeObject<List<EstimatePriceClass>>(priceList.ToString());
                        foreach (var timeProduct in timeEstimateProducts)
                        {
                            foreach (var priceProduct in priceEstimateProducts)
                            {
                                if ((priceProduct.cabService.ToLower() == timeProduct.cabServiceDisplayName.ToLower()) &&
                                    (priceProduct.displayName.ToLower() == timeProduct.displayName.ToLower()))
                                {
                                    providerResult.Add(new OAuthRestOfCabsProviderResult(priceProduct.cabService, priceProduct.displayName,
                                                 "", timeProduct.estimateTime, priceProduct.priceEstimate));
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
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }

    public class EstimateTimeClass
    {
        [JsonProperty("estimateTime")]
        public string estimateTime { get; set; }

        [JsonProperty("cabServiceDisplayName")]
        public string cabServiceDisplayName { get; set; }

        [JsonProperty("cabService")]
        public string cabService { get; set; }

        [JsonProperty("displayName")]
        public string displayName { get; set; }
    }

    public class EstimatePriceClass
    {
        [JsonProperty("cabServiceDisplayName")]
        public string cabService { get; set; }

        [JsonProperty("displayName")]
        public string displayName { get; set; }

        [JsonProperty("fare")]
        public string priceEstimate { get; set; }
    }
}
