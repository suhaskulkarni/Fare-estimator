using FareEstimate.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace FareEstimate.ViewModel
{
    public class HeaderPageViewModel : ViewModelBase
    {
        public HeaderPageViewModel()
        {
            resourceLoader = new ResourceLoader();
            Messenger.Default.Register<PageTypeEnumMessage>(this, PageTypeEventHandler);
        }

        ResourceLoader resourceLoader { get; set; }

        public string _pageType;

        public string PageType
        {
            get
            {
                return _pageType;
            }
            set
            {
                Set("PageType", ref _pageType, value, true);
            }
        }

        public void PageTypeEventHandler(PageTypeEnumMessage page)
        {
            switch(page)
            {
                case PageTypeEnumMessage.LocationPage:
                    PageType = resourceLoader.GetString("FindCabs");
                    break;

                case PageTypeEnumMessage.PriceEstimatesPage:
                    PageType = resourceLoader.GetString("CabsList");
                    break;

                default:
                    PageType = string.Empty;
                    break;
            }
        }
    }
}
