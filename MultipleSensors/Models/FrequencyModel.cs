using System;
using AxLEConnector.Helpers;

namespace MultipleSensors.Models
{
    public class FrequencyModel
    {

        public string Name { get; private set; }
        public StreamFrequency Frequency{ get; private set; }

        public FrequencyModel(StreamFrequency frequency)
        {
            Name = (int)frequency + "hz";
            Frequency = frequency;
        }
    }
}
