using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberFareEstimateFramework.OAuthService
{
    public class OAuthOlaService
    {
        public string RequestUri
        {
            get
            {
                return "https://devapi.olacabs.com/v1";
            }
        }

        public string RequestPriceEstimate
        {
            get
            {
                return "products";
            }
        }

        public string SourceLatitudeString
        {
            get
            {
                return "pickup_lat";
            }
        }

        public string SourceLatitude { get; set; }

        public string SourceLongitudeString
        {
            get
            {
                return "pickup_lng";
            }
        }

        public string SourceLongitude { get; set; }

        public string DestinationLatitudeString
        {
            get
            {
                return "drop_lat";
            }
        }

        public string DestinationLatitude { get; set; }

        public string DestinationLongitudeString
        {
            get
            {
                return "drop_lng";
            }
        }

        public string DestinationLongitude { get; set; }

        public string VehicleCategoryString
        {
            get
            {
                return "category";
            }
        }
        
        public string VehicleCategoryMini
        {
            get
            {
                return "mini";
            }
        }

        public string VehicleCategorySedan
        {
            get
            {
                return "sedan";
            }
        }

        public string VehicleCategoryPrime
        {
            get
            {
                return "prime";
            }
        }

        public string ServerToken
        {
            get
            {
                //return "bhUg2dK0THDlVL93qe9KtMweCAbAuVy7k4c9J8Wv";
                return "b321092f76da4db8498d980b3c8e6928";
            }
        }
    }
}
