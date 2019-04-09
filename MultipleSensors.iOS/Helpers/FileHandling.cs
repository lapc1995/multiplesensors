using System;
using System.IO;
using MultipleSensors.Helpers;
using MultipleSensors.iOS.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHandling))]
namespace MultipleSensors.iOS.Helpers
{
    public class FileHandling : IFileHandling
    {
        public string GetStoragePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public string SetFile(string filename)
        {
            return Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)), DateTime.UtcNow.ToBinary() + filename + ".csv");
        }
    }
}
