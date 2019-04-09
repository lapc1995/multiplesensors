using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using AxLEConnector.Helpers;
using AxLEConnector.Services;
using MultipleSensors.Helpers;
using MultipleSensors.Services;
using Xamarin.Forms;

namespace MultipleSensors.ViewModels
{
    public class CalibrationPageViewModel : INotifyPropertyChanged
    {
        private State _state;
        private string _calibrationButtonText;
        //private readonly CalibrationService _calibratingService;

        public ObservableCollection<string> ConnectedSensors { get; set; }

        public State State
        {
            set
            {
                if(_state != value)
                {
                    _state = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("State"));

                }
            }

            get
            {
                return _state;
            }
        }

        public string CalibrationButtonText
        {
            set
            {
                if (_calibrationButtonText != value)
                {
                    _calibrationButtonText = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CalibrationButtonText"));

                }
            }

            get
            {
                return _calibrationButtonText;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CalibrateCommand => new Command(() => StartCalibration());

        public CalibrationPageViewModel()
        {
            _state = State.CONNECTED;
            ConnectedSensors = new ObservableCollection<string>();
            foreach (string s in Devices.Instance.GetSerials()) ConnectedSensors.Add(s);
            //_calibratingService = new CalibrationService(Devices.Instance.GetSerials());
            _calibrationButtonText = "Start Calibration";
        }

        private void StartCalibration()
        {
            State = State.RECORDING;
            CalibrationButtonText = "Calibrating";
            //_calibratingService.StartCalibration();
            MessagingCenter.Subscribe<FileWriterService>(this, MessageType.FINISHED_WRITING.ToString(), (sender) =>
            {
                State = State.CONNECTED;
                CalibrationButtonText = "Start Calibration";
                MessagingCenter.Unsubscribe<FileWriterService>(this, MessageType.FINISHED_WRITING.ToString());
            });
        }
    }
}
