using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberFareEstimateFramework.OAuthService
{
    public class OAuthRestOfCabsService
    {
        public string RequestUri
        {
            get
            {
                return "https://oyetaxi.co.in/codebase/v1";
            }
        }

        public string RequestTimeEstimate
        {
            get
            {
                return "taxiLocationApi.php";
            }
        }

        public string RequestPriceEstimate
        {
            get
            {
                return "fareCalculator.php";
            }
        }

        public string SourceLatitudeString
        {
            get
            {
                return "latitude";
            }
        }

        public string SourceLatitude { get; set; }

        public string SourceLongitudeString
        {
            get
            {
                return "longitude";
            }
        }

        public string SourceLongitude { get; set; }

        public string DestinationLatitudeString
        {
            get
            {
                return "destination_latitude";
            }
        }

        public string DestinationLatitude { get; set; }

        public string DestinationLongitudeString
        {
            get
            {
                return "destination_longitude";
            }
        }

        public string DestinationLongitude { get; set; }

        public string CityOfBooking { get; set; }

        public string SortByString
        {
            get
            {
                return "sortBy=0";
            }
        }

        public string VersionCodeString
        {
            get
            {
                return "version_code=8";
            }
        }
    }
}
