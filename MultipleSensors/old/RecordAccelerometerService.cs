using System;
using System.Collections.Concurrent;
using MultipleSensors.Models;

namespace MultipleSensors.Services
{
    public abstract class RecordAccelerometerService
    {

        public ConcurrentQueue<AccelerometerData> receivedData;

        RecordAccelerometerService()
        {
            receivedData = new ConcurrentQueue<AccelerometerData>();
        }

        public abstract void StartRecording();

        public abstract void StopRecording();
    }
}
