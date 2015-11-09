using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingMapsLocationDataParser
{
    public class UriGenerator
    {
        private static string BingMapsKey = "SYoGCp85W00fY9ap48Kh~Z5-J3KZs1KvZgfxJA61reA~AqWeAoyIiK0IYFuhUW7UciUW5wgiYjLtjGzHe_Pj6nsAFTlPhnisQD_JTlY7itwN";
        public static Uri GetLocationQueryUri(string query)
        {
            return new Uri(string.Format("http://dev.virtualearth.net/REST/v1/Locations?query={0}&maxResults=10&key={1}", query, BingMapsKey));
        }
    }
}
