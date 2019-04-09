using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Timers;
using MultipleSensors.Models;
using Xamarin.Essentials;

namespace MultipleSensors.Services
{
    public class CalibrationService
    {
        private const int MAXVIBRATIONS = 3;

        private ConcurrentQueue<object> _receivedData;
        private readonly List<string> _serials;
        private AccelerometerDataGetterService<RecordingAccParameters> _recordingService;
        private RecordPhoneAccelerometerService _recordingPhoneService;
        private FileWriterService _fileWriterService;
        private Timer _timer;
        private int _nVibrations;

        public CalibrationService(List<string> serials)
        {
            _serials = serials;
        }

        public void StartCalibration()
        {
            _receivedData = new ConcurrentQueue<object>();
            _recordingService = new AccelerometerDataGetterService<RecordingAccParameters>(ref _receivedData, _serials, "Calibration");
            _recordingPhoneService = new RecordPhoneAccelerometerService(ref _receivedData);
            _fileWriterService = new FileWriterService(ref _receivedData);
            _timer = new Timer(2000);
            _timer.Elapsed += TimeHandler;
            _fileWriterService.StartFetchingData();
            _recordingService.StartAccelerometerStream();
            _recordingPhoneService.StopAccelerometerStream();
            _timer.Enabled = true;
        }

        public void StopCalibration()
        {
            _recordingService.StopAccelerometerStream();
            _recordingPhoneService.StopAccelerometerStream();
            _fileWriterService.StopFetchingData();
        }

        private void TimeHandler(object source, ElapsedEventArgs e)
        {
            if (_nVibrations < MAXVIBRATIONS)
            {
                Vibration.Vibrate();
                _nVibrations++;
            } else {
                _timer.Stop();
                StopCalibration();
            }
        }
    }
}
