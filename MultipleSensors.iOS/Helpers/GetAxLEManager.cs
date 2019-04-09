using MultipleSensors.Helpers;
using MultipleSensors.iOS.Helpers;
using OpenMovement.AxLE.Comms;
using OpenMovement.AxLE.Comms.Interfaces;
using Plugin.BLE;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetAxLEManager))]
namespace MultipleSensors.iOS.Helpers
{
    public class GetAxLEManager : IGetAxLEManager
    {
        IAxLEManager IGetAxLEManager.GetAxLEManager()
        {
            //return new AxLEManager(new OpenMovement.AxLE.Comms.Bluetooth.Mobile.BluetoothManager(CrossBluetoothLE.Current));
            return new AxLEManager(new OpenMovement.AxLE.Comms.Bluetooth.Mobile.BluetoothManager());
        }
    }
}

