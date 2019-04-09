using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MultipleSensors.Helpers;

namespace MultipleSensors.Converters
{
    public class StateToDisabledOnRecordingConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((State)value)
            {
                case State.RECORDING:
                    return false;

                case State.PREPARING:
                    return false;

                case State.STOPPING:
                    return false;

                default:
                    return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? State.UNKNOWN : State.RECORDING;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
