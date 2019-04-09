using MultipleSensors.Models;
using NUnit.Framework;

namespace Tests.Models
{
    [TestFixture()]
    public class AccelerometerDataTests
    {
        [Test()]
        public void CreateAccelerometerData()
        {
            //Arrange
            uint id = 1;
            int sample = 2;
            string activity = "sit";
            string device = "EB5F8AB86756";
            long sampleReceivedTimeUnixMilliseconds = 1542474274;
            uint timestampRaw = 1254315;
            double recordingTimeSeconds = 2.5;
            long timestampUnixMilliseconds = 1542474274;
            double x = 2.1;
            double y = 4.1;
            double z = -0.1;
            int battery = 30;

            //Act
            var accelerometerData = new AccelerometerData(
                    id: id,
                    sample: sample,
                    activity: activity,
                    device: device,
                    sampleReceivedTimeUnixMilliseconds: sampleReceivedTimeUnixMilliseconds,
                    timestampRaw: timestampRaw,
                    recordingTimeSeconds: recordingTimeSeconds,
                    timestampUnixMilliseconds: timestampUnixMilliseconds,
                    x: x,
                    y: y,
                    z: z,
                    battery: battery
                );

            //Assert
            Assert.AreEqual(id, accelerometerData.Id);
            Assert.AreEqual(sample, accelerometerData.Sample);
            Assert.AreEqual(activity, accelerometerData.Activity);
            Assert.AreEqual(device, accelerometerData.Device);
            Assert.AreEqual(sampleReceivedTimeUnixMilliseconds, accelerometerData.SampleReceivedTimeUnixMilliseconds);
            Assert.AreEqual(timestampRaw, accelerometerData.TimestampRaw);
            Assert.AreEqual(recordingTimeSeconds, accelerometerData.RecordingTimeSeconds);
            Assert.AreEqual(x, accelerometerData.X);
            Assert.AreEqual(y, accelerometerData.Y);
            Assert.AreEqual(z, accelerometerData.Z);
            Assert.AreEqual(battery, accelerometerData.Battery);
        }
    }
}
