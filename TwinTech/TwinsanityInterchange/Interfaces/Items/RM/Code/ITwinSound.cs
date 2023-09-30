using System;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code
{
    public interface ITwinSound : ITwinItem
    {
        UInt32 Header { get; set; }
        Byte UnkFlag { get; set; }
        Byte FreqFac { get; set; }
        UInt16 Param1 { get; set; }
        UInt16 Param2 { get; set; }
        UInt16 Param3 { get; set; }
        UInt16 Param4 { get; set; }
        Byte[] Sound { get; set; }

        ushort GetFreq();
    }
}
