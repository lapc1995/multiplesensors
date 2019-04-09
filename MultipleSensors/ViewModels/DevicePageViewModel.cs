using System;
using System.ComponentModel;
using MultipleSensors.Helpers;
using System.Windows.Input;
using Xamarin.Forms;
using AxLEConnector.Services;
using AxLEConnector.Helpers;

namespace MultipleSensors.ViewModels
{
    public class DevicePageViewModel : INotifyPropertyChanged
    {
        private int _battery;
        private string _serial, _recordingButtonText;
        private State _state;
        private RecordAxLEAccelerometerService<RecordingAccParameters> _recordingService;
        private bool _showPopUp;

        public event PropertyChangedEventHandler PropertyChanged;

        public String Serial 
        {
            set
            {
                if(_serial != value)
                {
                    _serial = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Serial"));
                }
            }

            get
            {
                return _serial;
            }
        }

        public int Battery
        {
            set
            {
                if(_battery != value)
                {
                    _battery = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Battery"));

                }
            }

            get
            {
                return _battery;
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

        public ICommand RecordCommand => new Command(() =>
        {
            switch (State)
            {
                case State.CONNECTED:
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

        public DevicePageViewModel()
        { 
            _serial = Devices.Instance.SelectedDevice;
            _battery = Devices.Instance.GetBattery(Devices.Instance.SelectedDevice);
            _state = State.CONNECTED;
            _recordingButtonText = "Start Recording Accelerometer  Stream";
        }

        private void StartRecordingAccelerometerStream()
        {
            _recordingService = new RecordAxLEAccelerometerService<RecordingAccParameters>(Serial, Devices.Instance.Activity);
            _recordingService.StartRecording();
            Devices.Instance.PreparingDevices += PreparingDeviceEventHandler;
            RecordingButtonText = "Stop Recording Accelerometer  Stream";
        }

        private void StopRecordingAccelerometerStream()
        {
            _recordingService.StopRecording();
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
            return false;
        }

        private void DeviceErrorEventHandler(object sender, DeviceErrorEventArgs e)
        {
            State = State.ERROR;
        }

        private void DeviceNotInRangeEventHandler(object sender, DeviceEventArgs e)
        {
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
            State = State.CONNECTED;
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
