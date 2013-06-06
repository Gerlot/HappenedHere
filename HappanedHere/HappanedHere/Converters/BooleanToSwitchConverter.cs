using HappanedHere.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace HappanedHere
{
    public class BooleanToSwitchConverter : IValueConverter
    {
        private string FalseValue = AppResources.Off;
        private string TrueValue = AppResources.On;

        public object Convert(object value, Type targetType, object parameter,
              System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return FalseValue;
            else
                return ("On".Equals(value)) ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType,
               object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null ? value.Equals(TrueValue) : false;
        }
    }
}
