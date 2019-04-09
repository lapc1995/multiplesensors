using System;
using Android.Media;
using MultipleSensors.Helpers;
using MultipleSensors.Droid.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHandling))]
namespace MultipleSensors.Droid.Helpers
{
    public class FileHandling : IFileHandling
    {
        public string GetStoragePath()
        {
            Java.IO.File path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            Java.IO.File dir = new Java.IO.File(path.AbsolutePath + "/mulAccData");
            dir.Mkdirs();
            return dir.AbsolutePath;
        }

        public string SetFile(string filename)
        {
            Java.IO.File path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            Java.IO.File dir = new Java.IO.File(path.AbsolutePath + "/mulAccData");
            dir.Mkdirs();
            Java.IO.File file = new Java.IO.File(dir, DateTime.UtcNow.ToBinary() + filename + ".csv");
            if (!file.Exists())
            {
                file.CreateNewFile();
                file.Mkdir();
            }

            MediaScannerConnection.ScanFile(Android.App.Application.Context, new string[] { file.AbsolutePath }, null, null);

            return file.AbsolutePath;
        }
    }
}
