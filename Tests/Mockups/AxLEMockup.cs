using System;
using System.Threading.Tasks;
using OpenMovement.AxLE.Comms.Interfaces;
using OpenMovement.AxLE.Comms.Values;

namespace Tests.Mockups
{
    public class AxLEMockup : IAxLE
    {
        public AxLEMockup()
        {
        }

        public string DeviceId => "0";

        public string SerialNumber => "EB5F8AB86756";

        public int Battery => 50;

        public uint DeviceTime => 2304921;

        public EraseData EraseData => throw new NotImplementedException();

        public uint ConnectionInterval { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Cueing { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint CueingPeriod { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint EpochPeriod { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint GoalPeriodOffset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint GoalPeriod { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint GoalThreshold { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<AccBlock> AccelerometerStream;

        public Task<bool> Authenticate(string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> DebugDump()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task LEDFlash()
        {
            throw new NotImplementedException();
        }

        public Task<BlockDetails> ReadBlockDetails()
        {
            throw new NotImplementedException();
        }

        public Task ResetPassword()
        {
            throw new NotImplementedException();
        }

        public Task SetPassword(string password)
        {
            throw new NotImplementedException();
        }

        public Task StartAccelerometerStream(int rate = 0, int range = 0)
        {

            throw new NotImplementedException();
        }

        public Task StopAccelerometerStream()
        {
            throw new NotImplementedException();
        }

        public Task<OpenMovement.AxLE.Service.Models.EpochBlock> SyncCurrentEpochBlock()
        {
            throw new NotImplementedException();
        }

        public Task<OpenMovement.AxLE.Service.Models.EpochBlock[]> SyncEpochData(ushort lastBlock, uint? lastRtc = null, DateTimeOffset? lastSync = null)
        {
            throw new NotImplementedException();
        }

        public Task<OpenMovement.AxLE.Service.Models.EpochBlock[]> SyncEpochData(ushort readFrom, ushort readTo, uint? startRtc = null, DateTimeOffset? startTime = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDeviceState()
        {
            throw new NotImplementedException();
        }

        public Task VibrateDevice()
        {
            throw new NotImplementedException();
        }

        public Task<BlockDetails> WriteCurrentBlock(ushort blockNo)
        {
            throw new NotImplementedException();
        }
    }
}
