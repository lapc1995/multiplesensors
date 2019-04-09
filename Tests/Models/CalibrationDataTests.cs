using MultipleSensors.Models;
using NUnit.Framework;
using System;
namespace Tests.Models
{
    [TestFixture()]
    public class CalibrationDataTests
    {
        [Test()]
        public void CreateCalibrationData()
        {
            //Arrange
            string device = "EB5F8AB86756";
            double x = 2.0;
            double y = 2.1;
            double z = 2.0;

            //Act
            CalibrationData calibrationData = new CalibrationData(device, x, y, z);

            //Assert
            Assert.AreEqual(device, calibrationData.Device);
            Assert.AreEqual(x, calibrationData.X);
            Assert.AreEqual(y, calibrationData.Y);
            Assert.AreEqual(z, calibrationData.Z);
        }
    }
}
