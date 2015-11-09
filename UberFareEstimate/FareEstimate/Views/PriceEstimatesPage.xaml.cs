using FareEstimate.Common;
using FareEstimate.DataModel;
using FareEstimate.Enums;
using FareEstimate.Viewmodel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FareEstimate.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PriceEstimatesPage : Page
    {
        public PriceEstimatesPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel.ViewModelLocator.PriceEstimatesViewModel;
            Messenger.Default.Register<NavigateToPageMessage>(this, NavigationPageEventHandler);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Messenger.Default.Send<PageTypeEnumMessage>(PageTypeEnumMessage.PriceEstimatesPage);

            var dataContext = this.DataContext as Viewmodel.PriceEstimatesViewModel;
            if (e.Parameter != null)
            {
                CoordinatesDetailModel coordinates = e.Parameter as CoordinatesDetailModel;
                //CoordinatesDetailModel coordinates = new CoordinatesDetailModel();
                //coordinates.SourceLatitude = "12.9780275";
                //coordinates.SourceLongitude = "77.5701955";
                //coordinates.DestinationLatitude = "12.8728932";
                //coordinates.DestinationLongitude = "77.5945862";
                dataContext.GetCabFareEstimates(coordinates);
            }
            //if(e.Parameter != null)
            //{
                //CoordinatesDetailModel coordinates = e.Parameter as CoordinatesDetailModel;
            CoordinatesDetailModel coordinates = new CoordinatesDetailModel();
            coordinates.SourceLatitude = "12.9766637";
            coordinates.SourceLongitude = "77.5712556";
            coordinates.DestinationLatitude = "12.8789001";
            coordinates.DestinationLongitude = "77.6089869";
            coordinates.CityOfCoordinates = "Bangalore";
            dataContext.GetCabFareEstimates(coordinates);
            //}
        }

        public async void NavigationPageEventHandler(NavigateToPageMessage msg)
        {
            ResourceLoader resourceLoader = new ResourceLoader();
            await new MessageDialog(resourceLoader.GetString("UnableToGetCabsList")).ShowAsync();
            Frame.Navigate(typeof(LocationPage));
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataContext = this.DataContext as PriceEstimatesViewModel;
            if(sender != null)
            {
                if(sender is ComboBox)
                {
                    var selectedItem = (sender as ComboBox).SelectedItem;
                    SortCabType result;
                    if (selectedItem is CabsSortTypes)
                    {
                        Enum.TryParse<SortCabType>((selectedItem as CabsSortTypes).Sort, true, out result);
                        if (dataContext.SortCommand.CanExecute(result))
                        {
                            dataContext.SortCommand.Execute(result);
                        }
                    }
                }
            }
        }

        public void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = this.DataContext as PriceEstimatesViewModel;
            if(dataContext.RefreshPageCommand.CanExecute(sender))
            {
                dataContext.RefreshPageCommand.Execute(sender);
            }
        }

        private void CabListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dataContext = this.DataContext as PriceEstimatesViewModel;
            if (e.ClickedItem is CabsListDetailModel)
            {
                var cabType = (e.ClickedItem as CabsListDetailModel).CabProviderType;
                if (dataContext.LaunchCabAppCommand.CanExecute(cabType))
                {
                    dataContext.LaunchCabAppCommand.Execute(cabType);
                }
            }
        }
    }
}
