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
        internal List<ITwinSerializable> Items;

        // A requirement to provide the header path
        public PS2BD(String headerPath, String headerWritePath)
        {
            this.headerWritePath = headerWritePath;
            this.headerPath = headerPath;
            Header = new PS2BH();
            Items = new List<ITwinSerializable>();
        }

        public void AddRecord(String path, Byte[] data)
        {
            var headRec = new BHRecord
            {
                Length = data.Length,
                Path = path,
                Offset = GetLength()
            };
            Header.Records.Add(headRec);
            var item = new BaseTwinItem(data);
            Items.Add(item);
        }

        public void RemoveRecord(Int32 id)
        {
            if (Items.Count >= id) return;
            Items.RemoveAt(id);
            Header.Records.RemoveAt(id);
        }

        public Int32 GetLength()
        {
            return Items.Sum(i => i.GetLength());
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
                var item = new BaseTwinItem();
                reader.BaseStream.Position = record.Offset;
                item.Read(reader, record.Length);
                Items.Add(item);
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
                item.Write(writer);
            }
        }
    }
}
