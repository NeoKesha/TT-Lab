using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class TwinObjectTriggerScript
    {
        public UInt16 UnkTriggerValue { get; set; }
        public UInt16 TriggerScript { get; set; }
        public Byte ScriptCallerIndex { get; set; }

        public TwinObjectTriggerScript(UInt32 value)
        {
            UnkTriggerValue = (UInt16)(value & 0x3FF);
            TriggerScript = (UInt16)(value >> 0xA & 0x3FFF);
            ScriptCallerIndex = (Byte)(value >> 0x18 & 0x1);
        }

        public UInt32 Compress()
        {
            UInt32 result = UnkTriggerValue;
            result |= (UInt32)(TriggerScript & 0x3FFF << 0xA);
            result |= (UInt32)(ScriptCallerIndex & 0x1 << 0x18);

            return result;
        }
    }
}
