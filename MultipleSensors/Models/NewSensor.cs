namespace MultipleSensors.Models
{
    public class NewSensor
    {
        public string Serial { set; get; }
        public string Id { private set; get; }

        public NewSensor(string serial, string id)
        {
            Serial = serial;
            Id = id;
        }

        public NewSensor(string id) : this("", id) {}
    }
}
