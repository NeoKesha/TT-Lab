using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives
{
    public class PS2BD : ITwinSerializable
    {
        private PS2BH Header;
        private String headerPath;
        private String headerWritePath;
        public List<BDRecord> Items;

        // A requirement to provide the header path
        public PS2BD(String headerPath, String headerWritePath)
        {
            this.headerWritePath = headerWritePath;
            this.headerPath = headerPath;
            Header = new PS2BH();
            Items = new List<BDRecord>();
        }

        public Int32 GetLength()
        {
            return Items.Sum(i => i.Data.Length);
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            using (FileStream headerStream = new FileStream(headerPath, FileMode.Open, FileAccess.Read))
            using (BinaryReader headerReader = new BinaryReader(headerStream))
            {
                Header.Read(headerReader, (Int32)headerStream.Length);
            }
            foreach (var record in Header.Records)
            {
                reader.BaseStream.Position = record.Offset;
                var r = new BDRecord(record, reader.ReadBytes(record.Length));
                Items.Add(r);
            }
        }

        public void Write(BinaryWriter writer)
        {
            using (FileStream stream = new FileStream(headerWritePath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter headerWriter = new BinaryWriter(stream))
            {
                Header.Write(headerWriter);
            }
            foreach (var item in Items)
            {
                writer.Write(item.Data);
            }
        }
    }
}
