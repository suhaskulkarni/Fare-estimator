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

        private static string TFSCabImagePath = "/Assets/Cabs/tfs.png";

        private static string MeruCabImagePath = "/Assets/Cabs/meru.png";

        public static string GetCabTypeIcon(CabType cabType)
        {
            switch(cabType)
            {
                case CabType.ola:
                    return OlaCabImagePath;
                case CabType.uber:
                    return UberCabImagePath;
                case CabType.taxiforsure:
                    return TFSCabImagePath;
                case CabType.meru:
                    return MeruCabImagePath;
                default:
                    return string.Empty;
            }
        }
    }
}
