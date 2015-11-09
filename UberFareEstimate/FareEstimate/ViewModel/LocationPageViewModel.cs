using BingMapsLocationDataParser;
using FareEstimate.DataModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FareEstimate.ViewModel
{
    public class LocationPageViewModel : ViewModelBase
    {
        public ObservableCollection<LocationDataModel> LocationList { get; set; }

        public LocationPageViewModel()
        {
            LocationList = new ObservableCollection<LocationDataModel>();
        }

        public void ClearList()
        {
            LocationList.Clear();
        }
        public async void ShowLocationSuggestions(string query)
        {
            var uri = UriGenerator.GetLocationQueryUri(query);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            var locationData = LocationDataParser.Parse(jsonString);

            if (locationData != null)
            {
                foreach (var resource in locationData.ResourceSets[0].Resources)
                {
                    var model = new LocationDataModel(resource.Name, resource.Point.Coordinates[0].ToString(), resource.Point.Coordinates[1].ToString());
                    LocationList.Add(model);
                }
            }
            else
            {
                //need to handle something
            }
        }

    }
}
