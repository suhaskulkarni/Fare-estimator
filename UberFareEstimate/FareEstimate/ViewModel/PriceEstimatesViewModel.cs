﻿using FareEstimate.Common;
using FareEstimate.DataModel;
using FareEstimate.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberFareEstimateFramework.OAuthService;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

namespace FareEstimate.Viewmodel
{
    public class PriceEstimatesViewModel : ViewModelBase
    {
        public PriceEstimatesViewModel()
        {
            _oauthUberService = new OAuthUberService();
            _oauthOlaService = new OAuthOlaService();
            _oauthUberProvider = new OAuthUberProvider();
            _oauthOlaProvider = new OAuthOlaProvider();
            _oauthOlaProviderList = new List<OAuthOlaProviderResult>();
            _oauthUberProviderList = new List<OAuthUberProviderResult>();

            ResourceContentLoader = new ResourceLoader();
        }

        private OAuthUberService _oauthUberService;

        private OAuthOlaService _oauthOlaService;

        private OAuthUberProvider _oauthUberProvider;

        private OAuthOlaProvider _oauthOlaProvider;

        public List<OAuthOlaProviderResult> _oauthOlaProviderList { get; set; }

        public List<OAuthUberProviderResult> _oauthUberProviderList { get; set; }

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

        public ResourceLoader ResourceContentLoader { get; set; }

        public async void GetCabFareEstimates(CoordinatesDetailModel coordinates)
        {
            IsInProgress = true;
            _oauthUberService.SourceLatitude = coordinates.SourceLatitude;
            _oauthUberService.SourceLongitude = coordinates.SourceLongitude;
            _oauthUberService.DestinationLatitude = coordinates.DestinationLatitude;
            _oauthUberService.DestinationLongitude = coordinates.DestinationLongitude;

            _oauthOlaService.SourceLatitude = coordinates.SourceLatitude;
            _oauthOlaService.SourceLongitude = coordinates.SourceLongitude;
            _oauthOlaService.DestinationLatitude = coordinates.DestinationLatitude;
            _oauthOlaService.DestinationLongitude = coordinates.DestinationLongitude;

            _oauthUberProviderList = await _oauthUberProvider.GetFareEstimates(_oauthUberService);

            if (_oauthUberProviderList != null)
            {
                foreach (var uberProvider in _oauthUberProviderList)
                {
                    CabsListGroup.Add(new CabsListDetailModel(CommonImageSource.GetCabTypeIcon(CabType.Uber), uberProvider.DisplayName,
                                      uberProvider.CurrencyCode, uberProvider.LowPriceEstimate, uberProvider.HighPriceEstimate,
                                      uberProvider.Distance, (Convert.ToInt32(uberProvider.TimeEstimate) / 60).ToString(), Convert.ToInt32(uberProvider.HighPriceEstimate)));
                }

                //TODO: Yet to get app token from Ola.
                //_oauthOlaProviderList = await _oauthOlaProvider.GetFareEstimates(_oauthOlaService);
                //foreach (var olaProvider in _oauthOlaProviderList)
                //{
                //    CabsListGroup.Add(new CabsListDetailModel(CommonImageSource.GetCabTypeIcon(CabType.Ola), olaProvider.DisplayName,
                //                      olaProvider.CurrencyCode, olaProvider.LowPriceEstimate, olaProvider.HighPriceEstimate,
                //                      olaProvider.Distance, olaProvider.TimeEstimate));
                //}
                CabsListGroup.OrderBy(x => x.HighPriceEstimateInteger).ToList();
            }
            else
            {
                Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage());
            }
            IsInProgress = false;
        }

    }
}