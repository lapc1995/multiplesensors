﻿using System.Threading.Tasks;
using System.Linq;

namespace OpenMovement.AxLE.Comms.Commands.V1
{
    public class DebugDump : AxLECommand<string>
    {
        public override async Task SendCommand()
        {
            await Device.TxUart("D");
        }

        protected override bool LookForEnd()
        {
            return true; // Wait until timeout
        }

        protected override string ProcessResult()
        {
            return string.Join("", Data.ToArray());
        }
    }
}
