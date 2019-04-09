using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using MultipleSensors.Helpers;
using Xamarin.Forms;

namespace MultipleSensors.Services
{
    public class FileWriterService
    {
        private readonly ConcurrentQueue<object> _queue;
        private  bool _fetching;

        private StreamWriter _writer;
        private CsvWriter _csvWriter;

        private IFileHandling _fileHandling;

        public FileWriterService(ref ConcurrentQueue<object> queue)
        {
            _queue = queue;
            _fetching = false;
            _fileHandling = DependencyService.Get<IFileHandling>();
        }

        public void StartFetchingData()
        { 
            _writer = new StreamWriter(_fileHandling.SetFile(App.activity));
            _csvWriter = new CsvWriter(_writer, false);
            _writer.AutoFlush = true;
            _fetching = true;

            Task.Run(() => FetchData());
        }

        public void StopFetchingData()
        {
            _fetching = false;
        }

        private void FetchData()
        {
            while (_fetching || _queue.Count != 0)
            {
                while (_queue.TryDequeue(out object data))
                {
                    _csvWriter.WriteRecord(data);
                    _csvWriter.NextRecord();
                };
                Thread.Sleep(2);
            }

            if(!_fetching && _queue.Count == 0)
            {
                _writer.Dispose();
                MessagingCenter.Send(this, MessageType.FINISHED_WRITING.ToString());
            }
        }
    }
}
