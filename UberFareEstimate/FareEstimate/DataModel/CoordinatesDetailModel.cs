using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FareEstimate.DataModel
{
    public class CoordinatesDetailModel
    {
        public string SourceLatitude { get; set; }

        public string SourceLongitude { get; set; }

        public string DestinationLatitude { get; set; }

        public string DestinationLongitude { get; set; }

        public string CityOfCoordinates { get; set; }
    }
}
