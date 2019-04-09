using System;
namespace MultipleSensors.Models
{
    public class CalibrationData
    {
        public string Device { private set; get; }
        public double X { private set; get; }
        public double Y { private set; get; }
        public double Z { private set; get; }

        public CalibrationData(string device, double x, double y, double z)
        {
            Device = device;
            X = x;
            Y = y;
            Z = z;
        }
    }
}
