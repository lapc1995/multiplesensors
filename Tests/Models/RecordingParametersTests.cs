

using System;
using System.Collections.Concurrent;
using MultipleSensors.Models;
using NUnit.Framework;
using OpenMovement.AxLE.Comms.Values;
using Tests.Mockups;

namespace Tests.Models
{
    [TestFixture]
    public class RecordingParametersTests
    {
        [Test] //[1,3,19]
        public void NotRecordingNotSetStreamCheck()
        {
            //Arrange
            ConcurrentQueue<AccelerometerData> queue = new ConcurrentQueue<AccelerometerData>();
            RecordingParameters rp = new RecordingParameters(ref queue, new AxLEMockup());
            rp._recording = false;
            rp._sentStreamCheck = false;

            AccBlock accBlock = new AccBlock
            {
                Timestamp = 1
            };

            //Act
            rp.HandleAccelerometerStreamAsync(new object(), accBlock);

            //Assert
            Assert.IsTrue(rp._sentStreamCheck);
        }

        [Test] //[1, 2, 4, 6, 7, 9, 10, 12]!
        public void NoSamples1()
        {
            //Arrange
            ConcurrentQueue<AccelerometerData> queue = new ConcurrentQueue<AccelerometerData>();
            RecordingParameters rp = new RecordingParameters(ref queue, new AxLEMockup());
            rp._recording = true;

            AccBlock accBlock = new AccBlock
            {
                Timestamp = 1,
                Samples = new Int16[0][]
            };

            //Act
            rp.HandleAccelerometerStreamAsync(new object(), accBlock);

            //Assert
            Assert.AreEqual(queue.Count, 0);
        }

        [Test] //[1, 2, 4, 7, 9, 10, 12]!
        public void NoSamples2()
        {
            //Arrange
            ConcurrentQueue<AccelerometerData> queue = new ConcurrentQueue<AccelerometerData>();
            RecordingParameters rp = new RecordingParameters(ref queue, new AxLEMockup());
            rp._recording = true;
            rp._firstTimestampSet = true;

            AccBlock accBlock = new AccBlock
            {
                Timestamp = 1,
                Samples = new Int16[0][]
            };

            //Act
            rp.HandleAccelerometerStreamAsync(new object(), accBlock);

            //Assert
            Assert.AreEqual(queue.Count, 0);
        }

        [Test] //[1, 2, 4, 6, 7, 8, 9, 10, 12]!
        public void NoSamples3()
        {
            //Arrange
            ConcurrentQueue<AccelerometerData> queue = new ConcurrentQueue<AccelerometerData>();
            RecordingParameters rp = new RecordingParameters(ref queue, new AxLEMockup());
            rp._recording = true;
            rp._firstTimestampSet = true;

            AccBlock accBlock = new AccBlock
            {
                Timestamp = 1,
                Samples = new Int16[0][]
            };

            //Act
            rp.HandleAccelerometerStreamAsync(new object(), accBlock);

            //Assert
            Assert.AreEqual(queue.Count, 0);
        }

    }
}
