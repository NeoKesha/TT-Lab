using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;
namespace Twinsanity.TwinsanityInterchange.Implementations.Base
{
    public class BaseTwinLevel : ITwinSection
    {
        protected List<ITwinItem> Items { get; }
        protected UInt32 magicNumber;
        protected Dictionary<UInt32, Type> idToClassDictionary = new Dictionary<uint, Type>();
        public BaseTwinLevel()
        {
            Items = new List<ITwinItem>();
        }
        public void AddItem(ITwinItem item)
        {
            Items.Add(item);
        }

        public uint GetID()
        {
            throw new NotImplementedException();
        }

        public ITwinItem GetItem(uint id)
        {
            return Items.Where(item => item.GetID() == id).FirstOrDefault();
        }

        public int GetLength()
        {
            return 12 + Items.Count * 12 + GetContentLength();
        }
        public int GetContentLength()
        {
            Int32 length = 0;
            foreach (ITwinItem item in Items)
            {
                length += item.GetLength();
            }
            return length;
        }

        public void Read(BinaryReader reader, int length)
        {
            magicNumber = reader.ReadUInt32();
            UInt32 itemsCount = reader.ReadUInt32();
            UInt32 streamLength = reader.ReadUInt32();
            Record[] records = new Record[itemsCount];
            for (int i = 0; i < itemsCount; ++i)
            {
                Record record = new Record();
                record.Read(reader, 12);
                records[i] = record;
            }
            Items.Clear();
            for (int i = 0; i < itemsCount; ++i)
            {
                ITwinItem item = null;
                if (idToClassDictionary.ContainsKey(records[i].ItemId))
                {
                    Type type = idToClassDictionary[records[i].ItemId];
                    item = (ITwinItem)Activator.CreateInstance(type);
                }
                else
                {
                    item = new BaseTwinItem();

                }
                reader.BaseStream.Position = records[i].Offset;
                item.Read(reader, (Int32)records[i].Size);
                item.SetID(records[i].ItemId);
                Items.Add(item);
            }
        }

        public void RemoveItem(uint id)
        {
            ITwinItem listItem = GetItem(id);
            if (listItem != null)
            {
                Items.Remove(listItem);
            }
        }

        public void SetID(uint id)
        {
            throw new NotImplementedException();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(magicNumber);
            writer.Write(Items.Count);
            writer.Write(GetContentLength());
            Record record = new Record();
            record.Offset = (UInt32)(12 + Items.Count * 12);
            foreach (ITwinItem item in Items)
            {
                record.Size = (UInt32)item.GetLength();
                record.ItemId = item.GetID();
                record.Write(writer);
                record.Offset += record.Size;
            }
            foreach (ITwinItem item in Items)
            {
                item.Write(writer);
            }
        }

        public Type IdToClass(uint id)
        {
            return idToClassDictionary[id];
        }
    }
}
