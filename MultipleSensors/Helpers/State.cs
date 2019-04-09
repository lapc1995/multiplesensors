using System;
namespace MultipleSensors.Helpers
{
    public enum State
    {
        CONNECTING, 
        CONNECTED, 
        RECORDING, 
        LOST_CONNECTION, 
        ERROR, 
        UNKNOWN, 
        PREPARING,
        STOPPING
    }
}
