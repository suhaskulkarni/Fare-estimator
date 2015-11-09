using FareEstimate.Common;
using FareEstimate.DataModel;
using FareEstimate.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberFareEstimateFramework.OAuthService;
using Windows.ApplicationModel.Resources;
using Windows.System;
using Windows.UI.Popups;

namespace FareEstimate.Viewmodel
{
    public class PriceEstimatesViewModel : ViewModelBase
    {
        public PriceEstimatesViewModel()
        {
            _oauthUberService = new OAuthUberService();
            _oauthOlaService = new OAuthOlaService();
            _oauthRestOfCabsService = new OAuthRestOfCabsService();
            _oauthUberProvider = new OAuthUberProvider();
            _oauthOlaProvider = new OAuthOlaProvider();
            _oauthRestOfCabsProvider = new OAuthRestOfCabsProvider();
            _oauthOlaProviderList = new List<OAuthOlaProviderResult>();
            _oauthUberProviderList = new List<OAuthUberProviderResult>();
            _oauthRestOfCabsProviderResult = new List<OAuthRestOfCabsProviderResult>();

            ResourceContentLoader = new ResourceLoader();
            SortOrder = CabsSortTypes.GetSortTypes();
            SortOrderItem = SortOrder[0];
            SortCommand = new RelayCommand<SortCabType>(SortCabsList);
            RefreshPageCommand = new RelayCommand(RefreshPage);
            LaunchCabAppCommand = new RelayCommand<CabType>(LaunchCabApp);
        }

        public RelayCommand<CabType> LaunchCabAppCommand { get; set; }

        public RelayCommand<SortCabType> SortCommand { get; set; }

        public RelayCommand RefreshPageCommand { get; set; }

        private OAuthUberService _oauthUberService;

        private OAuthOlaService _oauthOlaService;

        private OAuthRestOfCabsService _oauthRestOfCabsService;

        private OAuthUberProvider _oauthUberProvider;

        private OAuthOlaProvider _oauthOlaProvider;

        private OAuthRestOfCabsProvider _oauthRestOfCabsProvider;

        public List<OAuthOlaProviderResult> _oauthOlaProviderList { get; set; }

        public List<OAuthUberProviderResult> _oauthUberProviderList { get; set; }

        public List<OAuthRestOfCabsProviderResult> _oauthRestOfCabsProviderResult { get; set; }

        private bool _isInProgress = false;
        public bool IsInProgress
        {
            get
            {
                return _isInProgress;
            }
            set
            {
                Set("IsInProgress", ref _isInProgress, value, true);
            }
        }

        private ObservableCollection<CabsListDetailModel> _cabsListGroup = new ObservableCollection<CabsListDetailModel>();
        public ObservableCollection<CabsListDetailModel> CabsListGroup
        {
            get
            {
                return _cabsListGroup;
            }
            set
            {
                Set("CabsListGroup", ref _cabsListGroup, value, true);
            }
        }

        private ObservableCollection<CabsSortTypes> _sortOrder;
        public ObservableCollection<CabsSortTypes> SortOrder
        {
            get
            {
                return _sortOrder;
            }
            set
            {
                Set("SortOrder", ref _sortOrder, value, true);
            }
        }

        private CabsSortTypes _sortOrderItem;
        public CabsSortTypes SortOrderItem
        {
            get
            {
                return _sortOrderItem;
            }
            set
            {
                Set("SortOrderItem", ref _sortOrderItem, value, true);
            }
        }

        private string _distance;
        public string Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                Set("Distance", ref _distance, value, true);
            }
        }

        private SortCabType sortCabType { get; set; }

        public ResourceLoader ResourceContentLoader { get; set; }

        public async void GetCabFareEstimates(CoordinatesDetailModel coordinates, bool isRefreshRequired = false)
        {
            IsInProgress = true;
            _oauthUberService.SourceLatitude = coordinates.SourceLatitude;
            _oauthUberService.SourceLongitude = coordinates.SourceLongitude;
            _oauthUberService.DestinationLatitude = coordinates.DestinationLatitude;
            _oauthUberService.DestinationLongitude = coordinates.DestinationLongitude;

            _oauthRestOfCabsService.SourceLatitude = coordinates.SourceLatitude;
            _oauthRestOfCabsService.SourceLongitude = coordinates.SourceLongitude;
            _oauthRestOfCabsService.DestinationLatitude = coordinates.DestinationLatitude;
            _oauthRestOfCabsService.DestinationLongitude = coordinates.DestinationLongitude;
            _oauthRestOfCabsService.CityOfBooking = coordinates.CityOfCoordinates;

            _oauthUberProviderList = await _oauthUberProvider.GetFareEstimates(_oauthUberService);

            _oauthRestOfCabsProviderResult = await _oauthRestOfCabsProvider.GetFareEstimates(_oauthRestOfCabsService);

            if (_oauthUberProviderList != null || _oauthRestOfCabsProviderResult != null)
            {
                CabsListGroup = new ObservableCollection<CabsListDetailModel>();
                foreach (var uberProvider in _oauthUberProviderList)
                {
                    if (String.IsNullOrWhiteSpace(Distance))
                    {
                        Distance = String.Format(uberProvider.Distance + " " + Constants.Miles);
                    }
                    CabsListGroup.Add(new CabsListDetailModel(CommonImageSource.GetCabTypeIcon(CabType.uber), uberProvider.DisplayName,
                                  uberProvider.CurrencyCode, uberProvider.LowPriceEstimate, uberProvider.HighPriceEstimate,
                                  (Convert.ToInt32(uberProvider.TimeEstimate) / 60).ToString(), Convert.ToInt32(uberProvider.HighPriceEstimate), CabType.uber));
                }

                foreach (var cabsProvider in _oauthRestOfCabsProviderResult)
                {
                    if (cabsProvider.cabServiceProvider.ToLower() != CabType.uber.ToString())
                    {
                        string priceString;
                        CabType cabType;
                        Enum.TryParse<CabType>(cabsProvider.cabServiceProvider.Replace(" ", "").ToLower(), out cabType);
                        if (cabsProvider.PriceEstimate == Constants.OutOfRangePrice)
                        {
                            priceString = ResourceContentLoader.GetString("NotAvailable");
                        }
                        else
                        {
                            priceString = cabsProvider.PriceEstimate;
                        }
                        CabsListGroup.Add(new CabsListDetailModel(CommonImageSource.GetCabTypeIcon(cabType), cabsProvider.DisplayName,
                                    "INR", priceString, "", cabsProvider.TimeEstimate,
                                    Convert.ToInt32(cabsProvider.PriceEstimate), cabType));
                    }
                }
            }
            else
            {
                Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage());
                return;
            }

            if (isRefreshRequired)
            {
                await Task.Delay(1000);
            }

            if (isRefreshRequired)
            {
                if (sortCabType != null)
                {
                    if (sortCabType == SortCabType.Price)
                        CabsListGroup = new ObservableCollection<CabsListDetailModel>(CabsListGroup.OrderBy(x => x.HighPriceEstimateInteger).ToList());
                    else if (sortCabType == SortCabType.ETA)
                        CabsListGroup = new ObservableCollection<CabsListDetailModel>(CabsListGroup.OrderBy(x => x.EstimatedTimeInteger).ToList());
                }
            }
            else
            {
                CabsListGroup = new ObservableCollection<CabsListDetailModel>(CabsListGroup.OrderBy(x => x.HighPriceEstimateInteger).ToList());
            }
            IsInProgress = false;
        }

        public void SortCabsList(SortCabType cabType)
        {
            switch (cabType)
            {
                case SortCabType.Price:
                    sortCabType = SortCabType.Price;
                    CabsListGroup = new ObservableCollection<CabsListDetailModel>(CabsListGroup.OrderBy(x => x.HighPriceEstimateInteger).ToList());
                    break;
                case SortCabType.ETA:
                    sortCabType = SortCabType.ETA;
                    CabsListGroup = new ObservableCollection<CabsListDetailModel>(CabsListGroup.OrderBy(x => x.EstimatedTimeInteger).ToList());
                    break;
                default:
                    break;
            }
        }

        public async void RefreshPage()
        {
            if (_oauthUberProvider != null)
            {
                CoordinatesDetailModel coordinatesDetailModel = new CoordinatesDetailModel();
                coordinatesDetailModel.SourceLatitude = _oauthUberService.SourceLatitude;
                coordinatesDetailModel.SourceLongitude = _oauthUberService.SourceLongitude;
                coordinatesDetailModel.DestinationLatitude = _oauthUberService.DestinationLatitude;
                coordinatesDetailModel.DestinationLongitude = _oauthUberService.DestinationLongitude;
                coordinatesDetailModel.CityOfCoordinates = _oauthRestOfCabsService.CityOfBooking;
                GetCabFareEstimates(coordinatesDetailModel, true);
            }
        }

        public async void LaunchCabApp(CabType cabType)
        {
            switch (cabType)
            {
                case CabType.uber:
                    await Launcher.LaunchUriAsync(new Uri(String.Format("uber://?action=setPickup&pickup=my_location&client_id=7nso5f5k7aFoXfbX09xzNYHYgNd0vbSK")));
                    break;
                case CabType.ola:
                    await Launcher.LaunchUriAsync(new Uri(String.Format("olacabs://")));
                    break;
                default:
                    break;
            }
        }
    }
}
