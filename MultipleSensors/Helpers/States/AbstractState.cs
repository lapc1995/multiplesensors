namespace MultipleSensors.Helpers.States
{
    public abstract class AbstractState
    {
        public string Name {get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
