using MultipleSensors.Helpers;
using MultipleSensors.Models;
using NUnit.Framework;

namespace Tests.Models
{
    [TestFixture()]
    public class SensorStateTest
    {
        [Test()]
        public void CreateSensorState()
        {
            //Arrange
            string serial = "EB5F8AB86756";
            State state = State.CONNECTED;

            //Act
            SensorState sensorState = new SensorState(serial, state);

            //Assert
            Assert.AreEqual(serial, sensorState.Serial);
            Assert.AreEqual(state, sensorState.State);
        }

        [Test()]
        public void UpdateSensorState()
        {
            //Arrange
            SensorState sensorState = new SensorState("EB5F8AB86756", State.CONNECTED);

            //Act
            sensorState.Serial = "BBB";
            sensorState.State = State.RECORDING;

            //Assert
            Assert.AreEqual("BBB", sensorState.Serial);
            Assert.AreEqual(State.RECORDING, sensorState.State);
        }
    }
}
