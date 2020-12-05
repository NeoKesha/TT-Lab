using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives
{
    public class PS2MH : ITwinSerializable
    {
        public Int32 Interleave;
        public List<MHRecord> Records;

        public Int32 GetLength()
        {
            return 8 + Records.Sum(r => r.GetLength());
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            var recs = reader.ReadInt32();
            Interleave = reader.ReadInt32();
            for(var i = 0; i < recs; ++i)
            {
                var r = new MHRecord();
                r.Read(reader, 20);
                Records.Add(r);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Records.Count);
            writer.Write(Interleave);
            foreach (var r in Records)
            {
                r.Write(writer);
            }
        }
    }
}
