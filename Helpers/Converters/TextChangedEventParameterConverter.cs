using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_BarcodeScanner.Helpers.Converters
{
    class TextChangedEventParameterConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                TextChangedEventArgs obj = value as TextChangedEventArgs;
                return obj.NewTextValue;
            }

            
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
