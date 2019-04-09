using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AxLEConnector.Services;
using MultipleSensors.Models;
using Xamarin.Forms;

namespace MultipleSensors.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand ConnectCommand => new Command(async () => await Connect());
        public ICommand AddDeviceCommand => new Command(() => AddDevice());
        public ICommand RemoveDeviceCommand => new Command<string>(RemoveDevice);
      
        public ObservableCollection<NewSensor> Serials { get; set; }

        private int _idGenerator;

        public MainPageViewModel(){
            Serials = new ObservableCollection<NewSensor>
            {
                new NewSensor("EB5F8AB86756", _idGenerator++.ToString()),
                //new NewSensor("CE6F1AFA66B9", _idGenerator++.ToString()),
                new NewSensor("F3CEDE6CEA69", _idGenerator++.ToString()),
                //new NewSensor("FAD9CBE34210", _idGenerator++.ToString())
            };
        }


        private async Task Connect() 
        {
            foreach (NewSensor device in Serials)
            {
                if (!string.IsNullOrEmpty(device.Serial))
                    Devices.Instance.AddSensorSerial(device.Serial);
            }
            await App.NavigationService.NavigateAsync("SensorPage");
        }

        private void AddDevice()
        {
            Serials.Add(new NewSensor(_idGenerator++.ToString()));
        }

        private void RemoveDevice(string id)
        {
            NewSensor toBeRemoved = null;
            foreach(NewSensor ns in Serials)
            {
                if (ns.Id == id)
                {
                    toBeRemoved = ns;
                    break;
                }
            }
            Serials.Remove(toBeRemoved);
        }
    }
}
