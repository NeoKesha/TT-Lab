﻿using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;
using static Twinsanity.TwinsanityInterchange.Enumerations.Enums;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout
{
    public class PS2AnyTrigger : BaseTwinItem, ITwinTrigger
    {
        public TwinTrigger Trigger { get; set; }
        public UInt16[] TriggerScripts { get; set; }
        public PS2AnyTrigger()
        {
            TriggerScripts = new UInt16[4];
            Trigger = new TwinTrigger();
        }

        public override int GetLength()
        {
            return Trigger.GetLength() + 8;
        }

        public override void Read(BinaryReader reader, int length)
        {
            Trigger.Read(reader, length);
            for (int i = 0; i < 4; ++i)
            {
                TriggerScripts[i] = reader.ReadUInt16();
            }
        }

        public override void Write(BinaryWriter writer)
        {
            Trigger.Write(writer);
            for (int i = 0; i < 4; ++i)
            {
                writer.Write(TriggerScripts[i]);
            }
        }

        public override String GetName()
        {
            return $"Trigger {id:X}";
        }
    }
}
