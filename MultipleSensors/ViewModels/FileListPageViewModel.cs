using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MultipleSensors.Helpers;
using MultipleSensors.Models;
using Xamarin.Forms;

namespace MultipleSensors.ViewModels
{
    public class FileListPageViewModel
    {
        public ObservableCollection<CsvFile> FileList { get; set; }

        public ICommand ShareCommand => new Command<string>(ShareFile); 

        public FileListPageViewModel()
        {
            FileList = new ObservableCollection<CsvFile>();
            GetFiles();
        }

        private void GetFiles()
        {
            IFileHandling fileHandling = DependencyService.Get<IFileHandling>();
            Task.Run(() =>
            {
                string[] files = Directory.GetFiles(fileHandling.GetStoragePath(), "*.csv");
                for(int i = files.Length-1; i >= 0; i--)
                {
                    FileList.Add(new CsvFile(files[i]));
                }
            });
        }

        public void ShareFile(string path)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var share = DependencyService.Get<IShare>();
                await share.Show("Share CSV File", Path.GetFileName(path), path);
            });
        }
    }
}
