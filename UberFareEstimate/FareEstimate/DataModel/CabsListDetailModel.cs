using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FareEstimate.DataModel
{
    public class CabsListDetailModel
    {
        public CabsListDetailModel(string cabTypeIcon, string displayName, string currencyCode, string lowPriceEstimate, 
                                   string highPriceEstimate, string distance, string estimatedTime, int highPriceEstimateInteger)
        {
            this.CabTypeIcon = cabTypeIcon;
            this.DisplayName = displayName;
            this.PriceEstimate = String.Format(currencyCode + lowPriceEstimate + "-" + highPriceEstimate);
            this.Distance = String.Format(distance + "mi");
            this.EstimatedTime = String.Format(estimatedTime + "mins");
            this.HighPriceEstimateInteger = highPriceEstimateInteger;
        }

        public string CabTypeIcon { get; set; }

        public string DisplayName { get; set; }

        public string PriceEstimate { get; set; }

        public string Distance { get; set; }

        public string EstimatedTime { get; set; }

        public int HighPriceEstimateInteger { get; set; }
    }
}
