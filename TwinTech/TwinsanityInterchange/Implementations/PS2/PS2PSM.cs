using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2
{
    public class PS2PSM : ITwinPSM
    {
        public List<ITwinPTC> PTCs { get; set; }

        public PS2PSM()
        {
            PTCs = new List<ITwinPTC>();
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
                var ptc = new PS2PTC();
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
