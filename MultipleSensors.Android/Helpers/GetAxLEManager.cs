using MultipleSensors.Helpers;
using OpenMovement.AxLE.Comms;
using OpenMovement.AxLE.Comms.Interfaces;
using Plugin.BLE;
using OpenMovement.AxLE.Comms.Bluetooth.Mobile.Android;
using MultipleSensors.Droid.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetAxLEManager))]
namespace MultipleSensors.Droid.Helpers
{
    public class GetAxLEManager : IGetAxLEManager
    {
        IAxLEManager IGetAxLEManager.GetAxLEManager()
        {
            return new AxLEManager(new BluetoothManager(CrossBluetoothLE.Current));
        }
    }
}
