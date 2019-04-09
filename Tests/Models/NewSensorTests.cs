using MultipleSensors.Models;
using NUnit.Framework;
using System;
namespace Tests.Models
{
    [TestFixture()]
    public class NewSensorTests
    {
        [Test()]
        public void CreateNewSensor_TwoArgs()
        {
            //Arrange
            string serial = "EB5F8AB86756";
            string id = "1";

            //Act
            NewSensor newSensor = new NewSensor(serial, id);

            //Assert
            Assert.AreEqual(serial, newSensor.Serial);
            Assert.AreEqual(id, newSensor.Id);
        }

        [Test()]
        public void CreateNewSensor_OneArg()
        {
            //Arrange
            string id = "1";

            //Act
            NewSensor newSensor = new NewSensor(id);

            //Assert
            Assert.AreEqual("", newSensor.Serial);
            Assert.AreEqual(id, newSensor.Id);
        }

        [Test()]
        public void UpdateSerial()
        {
            //Arrange
            NewSensor newSensor = new NewSensor("1");

            //Act
            newSensor.Serial = "EB5F8AB86756";

            //Assert
            Assert.AreEqual("EB5F8AB86756", newSensor.Serial);
        }
    }
}
