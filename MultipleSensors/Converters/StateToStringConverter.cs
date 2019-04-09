using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MultipleSensors.Helpers;

namespace MultipleSensors.Converters
{
    public class StateToStringConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // todo add localization
            switch ((State)value)
            {
                case State.CONNECTING:
                    return "Conneting";

                case State.CONNECTED:
                    return "Connected";

                case State.RECORDING:
                    return "Recording";

                case State.LOST_CONNECTION:
                    return "Lost Connection";

                case State.ERROR:
                    return "Error";

                case State.UNKNOWN:
                    return "Unknown";

                case State.PREPARING:
                    return "Preparing";

                case State.STOPPING:
                    return "Stopping";

                default:
                    return "No such state exists";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Conneting":
                    return State.CONNECTING;

                case "Connected":
                    return State.CONNECTED;

                case "Recording":
                    return State.RECORDING;

                case "Lost Connection":
                    return State.LOST_CONNECTION;

                case "Error":
                    return State.ERROR;

                case "Unknown":
                    return State.UNKNOWN;

                case "Preparing":
                    return State.PREPARING;

                case "Stopping":
                    return State.STOPPING;

                default:
                    return State.UNKNOWN;
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
