using MultipleSensors.Helpers;

namespace MultipleSensors.Models
{
    public class SensorState
    {
        public string Serial { set; get; }
        public State State { set; get; }

        public SensorState(string serial, State state)
        {
            Serial = serial;
            State = state;
        }

    }
}
