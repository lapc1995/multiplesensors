namespace MultipleSensors.Models
{
    public class CsvFile
    {
        public string Name { get; private set; }
        public string Path { get; private set; }

        public CsvFile(string path)
        {
            Name = System.IO.Path.GetFileName(path);
            Path = path;
        }
    }
}
