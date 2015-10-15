using FareEstimate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FareEstimate.Common
{
    public class CommonImageSource
    {
        private static string OlaCabImagePath = "/Assets/Cabs/ola.png";

        private static string UberCabImagePath = "/Assets/Cabs/Uber.png";

        public static string GetCabTypeIcon(CabType cabType)
        {
            switch(cabType)
            {
                case CabType.Ola:
                    return OlaCabImagePath;

                case CabType.Uber:
                    return UberCabImagePath;

                default:
                    return string.Empty;
            }
        }
    }
}
