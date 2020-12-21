using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Implementations.Base
{
    public class BaseTwinSection : BaseTwinItem, ITwinSection
    {
        protected List<ITwinItem> Items { get; }
        protected UInt32 magicNumber;
        protected Dictionary<UInt32, Type> idToClassDictionary = new Dictionary<uint, Type>();
        protected Type defaultType = typeof(BaseTwinItem);
        protected bool skip;
        protected Byte[] extraData;
        public BaseTwinSection()
        {
            Items = new List<ITwinItem>();
            skip = false;
            isLazy = false;
            isLoaded = true;
        }
        public BaseTwinSection(bool isLazy)
        {
            Items = new List<ITwinItem>();
            skip = false;
            SetIsLazy(isLazy);
            if (isLazy)
            {
                root = this;
            }
        }
        public void AddItem(ITwinItem item)
        {
            Items.Add(item);
        }

        public Int32 GetItemsAmount()
        {
            return Items.Count;
        }

        public virtual ITwinItem GetItem(Int32 index)
        {
            if (index >= Items.Count) return null;
            ITwinItem twinItem = Items[index];
            LoadItem(twinItem);
            return twinItem;
        }
        public UInt32 GetIdByIndex(Int32 index)
        {
            if (index >= Items.Count) {
                throw new IndexOutOfRangeException();
            }
            return Items[index].GetID();
        }

        public virtual T GetItem<T>(uint id) where T : ITwinItem
        {
            ITwinItem twinItem = Items.Where(item => item.GetID() == id).FirstOrDefault();
            LoadItem(twinItem);
            return (T)twinItem;
        }

        private void LoadItem(ITwinItem twinItem)
        {
            if (twinItem != null && !twinItem.GetIsLoaded() && isLazy)
            {
                BinaryReader reader = new BinaryReader(root.GetStream());
                reader.BaseStream.Position = twinItem.GetOriginalOffset();
                twinItem.Read(reader, twinItem.GetOriginalSize());
                twinItem.SetIsLoaded(true);
            }
        }

        public override int GetLength()
        {
            if (skip)
            {
                return 0;
            }
            else
            {
                return 12 + Items.Count * 12 + GetContentLength() + extraData.Length;
            }
        }
        public int GetContentLength()
        {
            Int32 length = 0;
            foreach (ITwinItem item in Items)
            {
                LoadItem(item);
                length += item.GetLength();
            }
            return length;
        }

        protected virtual UInt32 ProcessId(UInt32 id)
        {
            return id;
        }
        public override void Read(BinaryReader reader, int length)
        {
            if (this == root && isLazy)
            {
                int len = (int)reader.BaseStream.Length - (int)reader.BaseStream.Position;
                stream = new MemoryStream(reader.ReadBytes(len));
                reader.BaseStream.Position -= len;
            }
            if (length > 0)
            {
                Int64 baseOffset = reader.BaseStream.Position;
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
                int extraPos = (int)reader.BaseStream.Position - (int)baseOffset;
                for (int i = 0; i < itemsCount; ++i)
                {
                    ITwinItem item = null;
                    UInt32 mapperId = ProcessId(records[i].ItemId);
                    if (idToClassDictionary.ContainsKey(mapperId))
                    {
                        Type type = idToClassDictionary[mapperId];
                        item = (ITwinItem)Activator.CreateInstance(type);
                    }
                    else
                    {
                        item = (ITwinItem)Activator.CreateInstance(defaultType);
                    }
                    
                    item.SetIsLazy(isLazy);
                    item.SetRoot(root);
                    item.SetStream(stream);
                    item.SetID(records[i].ItemId);
                    item.SetOriginalOffset((int)records[i].Offset + (int)baseOffset);
                    item.SetOriginalSize((Int32)records[i].Size);

                    if (!isLazy)
                    {
                        reader.BaseStream.Position = records[i].Offset + baseOffset;
                        item.Read(reader, (Int32)records[i].Size);
                    } 
                    else
                    {
                        item.SetIsLoaded(false);
                    }
                    extraPos += (Int32)records[i].Size;
                    Items.Add(item);
                }
                extraData = reader.ReadBytes((Int32)(length - (extraPos)));
            }
            else
            {
                skip = true;
            }
        }

        public void RemoveItem<T>(uint id) where T : ITwinItem
        {
            ITwinItem listItem = GetItem<T>(id);
            if (listItem != null)
            {
                Items.Remove(listItem);
            }
        }

        protected virtual void PreprocessWrite()
        {

        }

        public override void Write(BinaryWriter writer)
        {
            if (!skip)
            {
                PreprocessWrite();
                writer.Write(magicNumber);
                writer.Write(Items.Count);
                writer.Write(GetContentLength());
                Record record = new Record();
                record.Offset = (UInt32)(12 + Items.Count * 12);
                foreach (ITwinItem item in Items)
                {
                    record.Size = (UInt32)item.GetLength();
                    record.ItemId = item.GetID();
                    LoadItem(item);
                    record.Write(writer);
                    record.Offset += record.Size;
                }
                foreach (ITwinItem item in Items)
                {
                    LoadItem(item);
                    item.Write(writer);
                }
                writer.Write(extraData);
            }
        }

        public Type IdToClass(uint id)
        {
            return idToClassDictionary[id];
        }

        public override String GetName()
        {
            return $"Section {id}";
        }
    }
}
