using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MultipleSensors.Helpers;

namespace MultipleSensors.Converters
{
    public class StateToColorConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((State)value)
            {
                case State.CONNECTING:
                    return Color.Default;

                case State.CONNECTED:
                    return Color.Default;

                case State.RECORDING:
                    return Color.Red;

                case State.LOST_CONNECTION:
                    return Color.Default;

                case State.ERROR:
                    return Color.Default;

                case State.UNKNOWN:
                    return Color.Default;

                case State.PREPARING:
                    return Color.Red;

                case State.STOPPING:
                    return Color.Red;

                default:
                    return "No such state exists";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return  State.UNKNOWN;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
