using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MultipleSensors.Helpers;

namespace MultipleSensors.Converters
{
    public class StateToVisibleOnConnectedConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((State)value)
            {
                case State.CONNECTED:
                    return true;

                case State.RECORDING:
                    return true;

                case State.PREPARING:
                    return true;

                case State.STOPPING:
                    return true;

                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return State.UNKNOWN;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}