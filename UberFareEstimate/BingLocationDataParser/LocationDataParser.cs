using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BingMapsLocationDataParser
{
    public class LocationDataParser
    {
        public static LocationData Parse (string jsonString)
        {
            return JsonConvert.DeserializeObject<LocationData>(jsonString);
        }
    }
}
