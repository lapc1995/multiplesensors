using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using AxLEConnector.Helpers;
using AxLEConnector.Services;
using MultipleSensors.Helpers;
using MultipleSensors.Models;
using Xamarin.Forms;

namespace MultipleSensors.ViewModels
{

    public class SensorPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<SensorState> ConnectedSensors { get; set; }

        private string _sensorsConnectText, _recordingButtonText, _activity;
        private decimal _sensorsConnectedPercentage;
        private bool _showPopUp;

        private State _state;

        private RecordAxLEAccelerometerService<RecordingAccParameters> _recordService;

        public decimal SensorsConnectedPercentage 
        {
            set
            {
                _sensorsConnectedPercentage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SensorsConnectedPercentage"));
            }

            get
            {
                return _sensorsConnectedPercentage;
            }
        }
       
        public string SensorsConnectedText 
        {
            set
            {
                if (_sensorsConnectText != value)
                {
                    _sensorsConnectText = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("SensorsConnectedText"));
                }
            }
        
            get
            {
                return _sensorsConnectText;
            }
        }

        public string RecordingButtonText
        {
            set
            {
                if (_recordingButtonText != value)
                {
                    _recordingButtonText = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("RecordingButtonText"));
                }
            }

            get
            {
                return _recordingButtonText;
            }
        }

        public bool ShowPopUp
        {
            get
            {
                return _showPopUp;
            }

            set 
            {
                if (_showPopUp != value)
                {
                    _showPopUp = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowPopUp"));
                }
            }
        }

        public State State
        {
            set
            {
                if (_state != value)
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

        public string Activity
        {
            get
            {
                return _activity;
            }

            set 
            {
                if (_activity != value)
                {
                    _activity = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Activity"));
                }
            }
        }

        public ICommand RecordCommand => new Command(() =>
        {
            switch(State)
            {
                case State.CONNECTED:
                    ShowPopUp = false;
                    StartRecordingAccelerometerStream();
                    break;

                case State.RECORDING:
                    StopRecordingAccelerometerStream();
                    break;

                default:
                    Console.Write("Wrong State");
                    break;
            }
        });

        public ICommand CalibrationCommand => new Command(() => App.NavigationService.NavigateAsync("CalibrationPage"));

        public ICommand RetryConnectionAllCommand => new Command(RetryConnectingAll);

        public ICommand RetryConnectionCommand => new Command<string>(RetryConnecting);

        public ICommand GoToFilesCommand => new Command(() => { App.NavigationService.NavigateAsync("FileListPage"); });

        public ICommand ShowPopUpCommand => new Command(() =>
        {
            if (State != State.RECORDING)
                ShowPopUp = true;
            else 
            {
                StopRecordingAccelerometerStream();
            }
        });

        public ICommand GoToSettingsCommand => new Command(() => { App.NavigationService.NavigateAsync("SettingsPage"); });

        public ICommand VibrateCommand => new Command(() => { Devices.Instance.VibrateAllDevices(); });

        public ICommand FlashCommand => new Command(() => { Devices.Instance.FlashAllDevicesLEDs(); });

        public SensorPageViewModel()
        {
            ConnectedSensors = new ObservableCollection<SensorState>();

            _sensorsConnectText = "Connected to " + ConnectedSensors.Count + " of " + Devices.Instance.GetSerials().Count;
            _recordingButtonText = "Start Recording Accelerometer  Stream";

            Devices.Instance.StartScanning += StartScanningEventHandler;

            Devices.Instance.DeviceNotInRange += DeviceNotInRangeEventHandler;
            Devices.Instance.DeviceError += DeviceErrorEventHandler;

            Devices.Instance.Connect();
        }

        private void StartRecordingAccelerometerStream()
        {
            Devices.Instance.Activity = Activity;
            _recordService = new RecordAxLEAccelerometerService<RecordingAccParameters>(Devices.Instance.GetSerials(), Devices.Instance.Activity);
            Devices.Instance.PreparingDevices += PreparingDeviceEventHandler;
            _recordService.StartRecording();
            RecordingButtonText = "Stop Recording Accelerometer  Stream";
        }

        private void StopRecordingAccelerometerStream()
        {
            _recordService.StopRecording();
        }

        public void GoToDevice(object selectedDevice)
        {
            Devices.Instance.SelectedDevice = ((SensorState)selectedDevice).Serial;
            RemoveEventHandlers();
            App.NavigationService.NavigateAsync("DevicePage");
        }

        private void RetryConnectingAll()
        {
            ConnectedSensors.Clear();
            SensorsConnectedPercentage = 0;
            SensorsConnectedText = "Connected to " + ConnectedSensors.Count + " of " + Devices.Instance.GetSerials().Count;
            Devices.Instance.PreparingDevices += PreparingDeviceEventHandler;
            Devices.Instance.RetryConnectingAll();
        }

        private void RetryConnecting(string serial)
        {
            Devices.Instance.PreparingDevices += PreparingDeviceEventHandler;
            Devices.Instance.RetryConnecting(serial);
        }

        public bool GoBack()
        {
            if (State == State.RECORDING)
                return true;
            ConnectedSensors.Clear();
            Devices.Instance.DisconnectAll();
            return false;
        }

        private void DeviceErrorEventHandler(object sender, DeviceErrorEventArgs e)
        {
            bool found = false;
            for (int i = 0; i < ConnectedSensors.Count && !found; i++)
            {
                if (ConnectedSensors[i].Serial == e.Serial)
                {
                    ConnectedSensors[i].State = State.ERROR;
                    found = true;
                }
            }

            if (!found)
                ConnectedSensors.Add(new SensorState(e.Serial, State.ERROR));

            State = State.ERROR;
        }

        private void DeviceNotInRangeEventHandler(object sender, DeviceEventArgs e)
        {
            bool found = false;
            for (int i = 0; i < ConnectedSensors.Count && !found; i++)
            {
                if (ConnectedSensors[i].Serial == e.Serial)
                {
                    ConnectedSensors[i].State = State.LOST_CONNECTION;
                    found = true;
                }
            }

            if (!found)
                ConnectedSensors.Add(new SensorState(e.Serial, State.LOST_CONNECTION));

            State = State.LOST_CONNECTION;
        }

        private void PreparingDeviceEventHandler(object sender, EventArgs e)
        {
            State = State.PREPARING;
            Devices.Instance.PreparingDevices -= PreparingDeviceEventHandler;
            Devices.Instance.AllDevicesStreaming += AllDevicesStreamingEventHandler;
        }

        private void AllDevicesStreamingEventHandler(object sender, EventArgs e)
        {
            State = State.RECORDING;
            Devices.Instance.AllDevicesStreaming -= AllDevicesStreamingEventHandler;
            Devices.Instance.InitiateStreamStop += InitiateStreamStopEventHandler;
        }

        private void InitiateStreamStopEventHandler(object sender, EventArgs e)
        {
            State = State.STOPPING;
            Devices.Instance.InitiateStreamStop -= InitiateStreamStopEventHandler;
            Devices.Instance.FinishedWritting += FinishedWrittingEventHandler;
        }

        private void FinishedWrittingEventHandler(object sender, EventArgs e)
        {
            State = State.CONNECTED;
            RecordingButtonText = "Start Recording Accelerometer  Stream";
            Devices.Instance.FinishedWritting -= FinishedWrittingEventHandler;
        }

        private void StartScanningEventHandler(object e, EventArgs args)
        {
            State = State.CONNECTING;
            Devices.Instance.StartScanning -= StartScanningEventHandler;
            Devices.Instance.DeviceConnected += DeviceConnectedEventHandler;
            Devices.Instance.FinishedScanning += FinishedScanningEventHandler;
        }

        private void DeviceConnectedEventHandler(object sender, DeviceConnectedEventArgs e)
        {
            bool found = false;
            for (int i = 0; i < ConnectedSensors.Count && !found; i++)
            {
                if (ConnectedSensors[i].Serial == e.Serial)
                {
                    ConnectedSensors[i].State = State.CONNECTED;
                    found = true;
                }
            }

            if (!found)
            {
                ConnectedSensors.Add(new SensorState(e.Serial, State.CONNECTED));
                int nTotal = Devices.Instance.GetSerials().Count;
                SensorsConnectedPercentage = (decimal)(ConnectedSensors.Count / (nTotal * 1.0));
                SensorsConnectedText = "Connected to " + ConnectedSensors.Count + " of " + nTotal;
            }
        }

        private void FinishedScanningEventHandler(object sender, EventArgs e)
        {
            State = State.CONNECTED;
            Devices.Instance.DeviceConnected -= DeviceConnectedEventHandler;
            Devices.Instance.FinishedScanning -= FinishedScanningEventHandler;
        }

        private void RemoveEventHandlers()
        {
            Devices.Instance.StartScanning -= StartScanningEventHandler;
            Devices.Instance.DeviceConnected -= DeviceConnectedEventHandler;
            Devices.Instance.FinishedScanning -= FinishedScanningEventHandler;
            Devices.Instance.FinishedWritting -= FinishedWrittingEventHandler;
            Devices.Instance.AllDevicesStreaming -= AllDevicesStreamingEventHandler;
            Devices.Instance.PreparingDevices -= PreparingDeviceEventHandler;
            Devices.Instance.InitiateStreamStop -= InitiateStreamStopEventHandler;
            Devices.Instance.DeviceNotInRange -= DeviceNotInRangeEventHandler;
            Devices.Instance.DeviceError -= DeviceErrorEventHandler;
        }
    }
}
 