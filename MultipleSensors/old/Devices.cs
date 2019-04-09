using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultipleSensors.Helpers;
using OpenMovement.AxLE.Comms.Exceptions;
using OpenMovement.AxLE.Comms.Interfaces;
using OpenMovement.AxLE.Comms.Values;
using Xamarin.Forms;

namespace Services.MultipleSensors
{
    public sealed class Devices
    {

        private static readonly Lazy<Devices> lazy =
            new Lazy<Devices>(() => new Devices());

        public static Devices Instance { get { return lazy.Value; } }

        private List<string> _serials;
        private Dictionary<string, IAxLE> _devices;
        private Dictionary<string, EventHandler<AccBlock>> _deviceEventHandlers;
        private List<string> _devicesToBeConnected;

        public string Activity { set; get; }

        public IAxLEManager manager;

        public int Rate { set; get; }
        public int Range { set; get; }

        public string SelectedDevice { set; get; }

        public StreamFrequency StreamFrequency { set; get; }

        private Devices()
        {
            _serials = new List<string>();
            _devices = new Dictionary<string, IAxLE>();
            _deviceEventHandlers = new Dictionary<string, EventHandler<AccBlock>>();
            StreamFrequency = StreamFrequency.HIGH;
        }

        public void AddSensorSerial(string serial)
        {
            _serials.Add(serial);
        }

        public List<string> GetSerials()
        {
            return new List<string>(_serials);
        }

        public IAxLE GetSensor(string serial) 
        {
            return _devices[serial];
        }

        public void AddSensor(IAxLE sensor)
        {
            _devices.Add(sensor.SerialNumber, sensor);
        }

        public List<IAxLE> GetAllSensors()
        {
            return _devices.Values.ToList();
        }

        public void SetDeviceAccelerometerEventHandler(string serial, EventHandler<AccBlock> handler)
        {
            _devices[serial].AccelerometerStream += handler;
            _deviceEventHandlers.Add(serial, handler);
        }

        public void RemoveDeviceAccelerometerEventHandler(string serial)
        {
            _devices[serial].AccelerometerStream -= _deviceEventHandlers[serial];
            _deviceEventHandlers.Remove(serial);
        }

        public void StartAccelerometers()
        {
            _devices.Keys.ToList().ForEach(k =>_devices[k].StartAccelerometerStream((int)StreamFrequency));
        }

        public void StopAccelerometers()
        {
            _devices.Keys.ToList().ForEach(k => _devices[k].StopAccelerometerStream());
        }

        public async Task Connect(List<string> serials)
        {
            if (manager == null)
                manager = DependencyService.Get<IGetAxLEManager>().GetAxLEManager();

            _devicesToBeConnected = serials;
            manager.DeviceFound += DeviceFoundHandler; 

            MessagingCenter.Send(this, MessageType.START_SCANNING.ToString());
            manager.SwitchToHighPowerScan();
            await manager.StartScan();
        }

        public async Task Connect()
        {
            await Connect(GetSerials());
        }

        public async Task Connect(string serial)
        {
            await Connect(new List<string>() { serial });
        }

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        private async void DeviceFoundHandler(object sender, string serial)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                if (_devicesToBeConnected.Contains(serial))
                {
                    _devicesToBeConnected.Remove(serial);
                    await manager.StopScan();
                    IAxLE device = await manager.ConnectDevice(serial);

                    await device.ResetPassword();
                    var pass = device.SerialNumber.Substring(device.SerialNumber.Length - 6);
                    if (await device.Authenticate(pass))
                    {
                        await device.UpdateDeviceState();
                        AddSensor(device);
                        MessagingCenter.Send(this, MessageType.DEVICE_CONNECTED.ToString(), serial);
                    }

                    if (_devicesToBeConnected.Count != 0)
                        await manager.StartScan();
                    else
                    {
                        await manager.SwitchToNormalMode();
                        MessagingCenter.Send(this, MessageType.FINISHED_SCANNING.ToString());
                    }
                }
            }
            catch (DeviceNotInRangeException)
            {
                await manager.StopScan();
                manager.DeviceFound -= DeviceFoundHandler;
                await manager.SwitchToNormalMode();
                MessagingCenter.Send(this, MessageType.DEVICE_CONNECTED.ToString(), serial);
                MessagingCenter.Send(this, MessageType.EXCEPTION_DEVICE_NOT_IN_RANGE.ToString(), serial);
            }
            catch (Exception ex)
            {
                await manager.StopScan();
                manager.DeviceFound -= DeviceFoundHandler;
                await manager.SwitchToNormalMode();
                MessagingCenter.Send(this, MessageType.DEVICE_CONNECTED.ToString(), serial);
                MessagingCenter.Send(this, MessageType.EXCEPTION.ToString(), new string[] { ex.ToString(), serial });
            }
        }

        public async void RetryConnecting(string serial)
        {
            _devices.Remove(serial);
            await Task.Run(() => Connect(serial));
        }

        public async Task RetryConnectingAll()
        {
            foreach (IAxLE device in _devices.Values)
                await manager.DisconnectDevice(device);
            _devices.Clear();
            await Task.Run(Connect);
        }

        public async Task Disconnect(string serial)
        {
            await manager.DisconnectDevice(_devices[serial]);
            _devices.Remove(serial);
            MessagingCenter.Send(this, MessageType.DEVICE_DISCONNECTED.ToString(), serial);
        }

        public async Task DisconnectAll()
        {
            foreach(IAxLE device in _devices.Values)
                await manager.DisconnectDevice(device);
            _devices.Clear();
        }

        public async Task<int> GetBatery(string serial)
        {
            await _devices[serial].UpdateDeviceState();
            return _devices[serial].Battery;
        }
    }s
}