using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.Xbox
{
    public class XboxPSM : ITwinSerializable
    {
        public List<XboxPTC> PTCs;

        public XboxPSM()
        {
            PTCs = new List<XboxPTC>();
        }

        public Int32 GetLength()
        {
            return PTCs.Sum(p => p.GetLength());
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            var startPos = reader.BaseStream.Position;
            while (reader.BaseStream.Position < startPos + length)
            {
                var ptc = new XboxPTC();
                ptc.Read(reader, 0);
                PTCs.Add(ptc);
            }
        }

        public void Write(BinaryWriter writer)
        {
            foreach (var ptc in PTCs)
            {
                ptc.Write(writer);
            }
        }
    }
}
