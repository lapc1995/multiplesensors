using System;
namespace MultipleSensors.Helpers
{
    public interface IFileHandling
    {
        string SetFile(string filename);

        string GetStoragePath();
    }
}
