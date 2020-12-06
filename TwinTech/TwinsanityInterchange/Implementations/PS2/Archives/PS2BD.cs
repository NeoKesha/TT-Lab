using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives
{
    public class PS2BD : ITwinSerializable
    {
        public PS2BH Header;
        public List<ITwinSerializable> Items;

        // A requirement to provide the header archive
        public PS2BD(PS2BH header)
        {
            Header = header;
            Items = new List<ITwinSerializable>();
        }

        public Int32 GetLength()
        {
            return Items.Sum(i => i.GetLength());
        }

        public void Read(BinaryReader reader, Int32 length)
        {
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
            foreach (var item in Items)
            {
                item.Write(writer);
            }
        }
    }
}
