using FareEstimate.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace FareEstimate.Common
{
    public class CabsSortTypes
    {
        public string Sort { get; set; }

        public SortCabType Type { get; set; }

        private static ResourceLoader resourceLoader = new ResourceLoader();

        static ObservableCollection<CabsSortTypes> sortTypes;
        public static ObservableCollection<CabsSortTypes> GetSortTypes()
        {
            if(sortTypes == null)
            {
                sortTypes = new ObservableCollection<CabsSortTypes>();
                sortTypes.Add(new CabsSortTypes() { Sort = resourceLoader.GetString("SortByPrice"), Type = SortCabType.Price });
                sortTypes.Add(new CabsSortTypes() { Sort = resourceLoader.GetString("SortByETA"), Type = SortCabType.ETA });
            }
            return sortTypes;
        }
    }
}
