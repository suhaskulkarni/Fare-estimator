using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberFareEstimateFramework.OAuthService
{
    public class OAuthUberService
    {
        public string RequestUri
        {
            get
            {
                return "https://api.uber.com/v1";
            }
        }

        public string ServerToken
        {
            get
            {
                return "AIjF1f_Q4J-Pk2QinH67qw8jtEROltOQkDCk6XSt";
            }
        }

        public string RequestPriceEstimate
        {
            get
            {
                return "estimates/price";
            }
        }

        public string RequestTimeEstimate
        {
            get
            {
                return "estimates/time";
            }
        }

        public string SourceLatitudeString
        {
            get
            {
                return "source_latitude";
            }
        }

        public string SourceLongitudeString
        {
            get
            {
                return "source_longitude";
            }
        }

        public string DestinationLatitudeString
        {
            get
            {
                return "destination_latitude";
            }
        }

        public string DestinationLongitudeString
        {
            get
            {
                return "destination_longitude";
            }
        }

        public string SourceLatitude { get; set; }

        public string SourceLongitude { get; set; }

        public string DestinationLatitude { get; set; }

        public string DestinationLongitude { get; set; }
    }
}
