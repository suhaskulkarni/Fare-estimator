using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FareEstimate.DataModel
{
    public class LocationDataModel
    {
        public string LocationName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public LocationDataModel(string locationName, string longitude, string latitude)
        {
            LocationName = locationName;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
