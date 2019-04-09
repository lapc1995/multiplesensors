using System;
using System.Collections.Concurrent;
using Xamarin.Essentials;

namespace MultipleSensors.Services
{
    public class RecordPhoneAccelerometerService
    {
        private readonly ConcurrentQueue<object> _receivedData;
        private uint _sampleId;
        private long _timestampFirst;
        private bool _firstTimestampSet;

        public RecordPhoneAccelerometerService(ref ConcurrentQueue<object> receivedData)
        {
            _receivedData = receivedData;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        public void StartAccelerometerStream()
        {
            _firstTimestampSet = false;
            ToggleAccelerometer();
        }

        public void StopAccelerometerStream()
        {
            ToggleAccelerometer();
        }

        private void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(SensorSpeed.UI);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine(fnsEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            long timestamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
            if (!_firstTimestampSet)
            {
                _timestampFirst = timestamp;
                _firstTimestampSet = true;
            }
            double recordingTime = (timestamp - _timestampFirst) / 1000.0;
            _receivedData.Enqueue(new Models.AccelerometerData(
                id: _sampleId,
                sample: (int)_sampleId,
                activity: "Calibration",
                device: "Smartphone",
                sampleReceivedTimeUnixMilliseconds: timestamp,
                timestampRaw: 0,
                recordingTimeSeconds: recordingTime,
                timestampUnixMilliseconds: timestamp,
                x: data.Acceleration.X,
                y: data.Acceleration.Y,
                z: data.Acceleration.Z
            ));
            _sampleId++;
        }
    }
}
