using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives
{
    public class PS2MB : ITwinSerializable
    {
        private PS2MH Header;
        private List<MBRecord> Items;
        private String headerPath;
        private String headerWritePath;

        // A requirement to provide the header path
        public PS2MB(String headerPath, String headerWritePath)
        {
            this.headerWritePath = headerWritePath;
            this.headerPath = headerPath;
            Header = new PS2MH();
            Items = new List<MBRecord>();
        }

        public void RemoveRecord(Int32 id)
        {
            if (id >= Items.Count) return;
            Items.RemoveAt(id);
            Header.Records.RemoveAt(id);
        }

        public void AddRecord(RecordType type, String name, Byte[] data, Int32 sampleRate)
        {
            if (name == "undefined")
            {
                throw new ArgumentException("Argument can not be 'undefined' because it is reserved!", "name");
            }
            var newHeadRec = new MHRecord
            {
                Type = type,
                Offset = Header.GetNewOffset(),
                SampleRate = sampleRate,
                Size = type == RecordType.MONO ? data.Length + 0x30 : data.Length,
                UnkInt = 0
            };
            Header.Records.Add(newHeadRec);
            var newRec = new MBRecord(newHeadRec)
            {
                TrackData = data,
                SampleRate = sampleRate,
                Name = new String(name.ToCharArray(0, Math.Min(name.Length, 0x10)))
            };
            if (newRec.Name.Length != 0x10)
            {
                for (var i = newRec.Name.Length; i < 0x10; ++i)
                {
                    newRec.Name += '\0';
                }
            }
            Items.Add(newRec);
        }

        public Int32 GetLength()
        {
            return Items.Sum(r => r.GetLength());
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            using (FileStream headerStream = new FileStream(headerPath, FileMode.Open, FileAccess.Read))
            using (BinaryReader headerReader = new BinaryReader(headerStream))
            {
                Header.Read(headerReader, (Int32)headerStream.Length);
            }
            foreach (var record in Header.GetSortedRecords())
            {
                reader.BaseStream.Position = record.Offset;
                var hasRec = false;
                var r = new MBRecord(record);
                r.Read(reader, record.Size);
                // Apparently "undefined" track dupes are not allowed
                if (r.Name != null && r.Name.Replace("\0", "") == "undefined")
                {
                    var undef = Items.Find(rec => rec.Name != null && rec.Name.Replace("\0", "") == "undefined");
                    if (undef != null) hasRec = true;
                }
                if (!hasRec)
                {
                    Items.Add(r);
                }
            }
        }

        public void Write(BinaryWriter writer)
        {
            using (FileStream stream = new FileStream(headerWritePath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter headerWriter = new BinaryWriter(stream))
            {
                Header.Write(headerWriter);
            }
            foreach (var r in Items)
            {
                r.Write(writer);
                // Padding
                while (writer.BaseStream.Position % 0x800 != 0)
                {
                    writer.Write((Byte)0);
                }
            }
        }

        public enum RecordType
        {
            MONO,
            STEREO,
            NULL
        }
    }
}
