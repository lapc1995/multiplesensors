using System;
using System.Collections.Concurrent;
using MultipleSensors.Helpers;
using OpenMovement.AxLE.Comms.Exceptions;
using OpenMovement.AxLE.Comms.Values;
using Xamarin.Forms;

namespace Old.Abstractions
{
    public abstract class AbstractRecordingParameters
    {
        protected ConcurrentQueue<object> queue;
        protected string _serial;
        protected string _activity;

        public bool Recording { set; get; }
        public bool Stop { set; get; }

        private bool _sentStreamCheck;


        protected AbstractRecordingParameters(){}

        public void SetParameters(ref ConcurrentQueue<object> queue, string serial, string activity)
        {
            this.queue = queue;
            _serial = serial;
            _activity = activity;
            Recording = false;
            Stop = false;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async void HandleAccelerometerStreamAsync(object sender, AccBlock accBlock)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            try
            {
                HandlerBehaviour(sender, accBlock);
                if (!_sentStreamCheck)
                {
                    MessagingCenter.Send(this, MessageType.DEVICE_STREAMING.ToString(), _serial);
                    _sentStreamCheck = true;
                }
            }
            catch (DeviceNotInRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public abstract void HandlerBehaviour(object sender, AccBlock accBlock);
    }
}
