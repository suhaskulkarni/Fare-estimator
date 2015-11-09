using FareEstimate.Enums;
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
                                   string highPriceEstimate, string estimatedTime, int highPriceEstimateInteger, CabType cabType)
        {
            this.CabTypeIcon = cabTypeIcon;
            this.DisplayName = displayName;
            if (string.IsNullOrWhiteSpace(highPriceEstimate))
            {
                this.PriceEstimate = String.Format(currencyCode + " " + lowPriceEstimate);
            }
            else
            {
                this.PriceEstimate = String.Format(currencyCode + " " + highPriceEstimate);
            }
            this.EstimatedTime = String.Format(estimatedTime + "mins");
            this.HighPriceEstimateInteger = highPriceEstimateInteger;
            this.EstimatedTimeInteger = Convert.ToInt32(estimatedTime);
            this.CabProviderType = cabType;
        }

        public string CabTypeIcon { get; set; }

        public string DisplayName { get; set; }

        public string PriceEstimate { get; set; }

        public string EstimatedTime { get; set; }

        public int HighPriceEstimateInteger { get; set; }

        public int EstimatedTimeInteger { get; set; }

        public CabType CabProviderType { get; set; }
    }
}
