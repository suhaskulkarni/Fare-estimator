using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FareEstimate.Converters
{
    public class B2VConverter : IValueConverter
    {
        public bool IsNegative { get; set; }
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (IsNegative && value is bool)
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            else if (value is bool)
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            else if (IsNegative && value is int)
                return (int)(value) > 0 ? Visibility.Collapsed : Visibility.Visible;
            else if (value is int)
                return (int)(value) > 0 ? Visibility.Visible : Visibility.Collapsed;
            else if (IsNegative && value is long)
                return (long)(value) > 0 ? Visibility.Collapsed : Visibility.Visible;
            else if (value is long)
                return (long)(value) > 0 ? Visibility.Visible : Visibility.Collapsed;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
