using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab
{
    public enum AgentLabVersion
    {
        PS2,
        Xbox,
        Demo
    }

    public interface ITwinBehaviourCommand : ITwinSerializable
    {
        public UInt32 Bitfield { get; set; }
        public UInt16 CommandIndex { get; set; }
        public List<UInt32> Arguments { get; set; }
        public UInt32 CommandSize { get; }
        public AgentLabVersion Version { get; }

        internal bool HasNext { get; set; }

        public void Read(BinaryReader reader, int length, IList<ITwinBehaviourCommand> commands);

        public void WriteText(StreamWriter writer, Int32 tabs = 0);

        public void ReadText(String line);
    }
}
