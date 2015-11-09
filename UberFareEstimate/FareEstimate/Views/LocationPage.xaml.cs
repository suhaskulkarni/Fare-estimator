using FareEstimate.DataModel;
using FareEstimate.Enums;
using FareEstimate.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class LocationPage : Page
    {
        public LocationPage()
        {
            this.InitializeComponent();
            this.DataContext = new LocationPageViewModel();
        }

        private LocationDataModel source;
        private LocationDataModel destination;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Messenger.Default.Send<PageTypeEnumMessage>(PageTypeEnumMessage.LocationPage);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            //tb.Visibility = Visibility.Collapsed;
            Flyout.ShowAttachedFlyout(tb);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            var dc = (this.DataContext as LocationPageViewModel);
            dc.ClearList();
            if (tb.Text.Length >= 3)
            {
                dc.ShowLocationSuggestions(tb.Text);
            }
        }

        private void SourceListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLocation = (LocationDataModel)(sender as ListBox).SelectedItem;
            if (selectedLocation == null)
                return;
            source = selectedLocation;
            tb_source.Text = source.LocationName;
            Flyout.GetAttachedFlyout(this.tb_source).Hide();
        }

        private void DestListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLocation = (LocationDataModel)(sender as ListBox).SelectedItem;
            if (selectedLocation == null)
                return;
            destination = selectedLocation;
            tb_dest.Text = destination.LocationName;
            Flyout.GetAttachedFlyout(this.tb_dest).Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            CoordinatesDetailModel coordinates = new CoordinatesDetailModel();
            coordinates.SourceLatitude = source.Latitude;
            coordinates.SourceLongitude = source.Longitude;
            coordinates.DestinationLatitude = destination.Latitude;
            coordinates.DestinationLongitude = destination.Longitude;
            rootFrame.Navigate(typeof(PriceEstimatesPage), coordinates);
        }
    }
}
