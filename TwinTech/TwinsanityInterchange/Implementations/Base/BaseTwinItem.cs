using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.Base
{
    public class BaseTwinItem : ITwinItem
    {
        protected UInt32 id;
        protected bool isLoaded;
        protected bool isLazy;
        protected ITwinItem root;
        protected MemoryStream stream;
        protected int originalPosition;
        protected int originalSize;
        Byte[] data;
        public BaseTwinItem()
        {
            data = new Byte[0];
            isLazy = false;
        }
        public BaseTwinItem(Byte[] data)
        {
            this.data = data;
            isLazy = false;
        }

        public void Dispose()
        {
            if (stream != null)
            {
                stream.Dispose();
            }
        }

        public virtual uint GetID()
        {
            return id;
        }

        public virtual Boolean GetIsLoaded()
        {
            return isLoaded;
        }

        public virtual int GetLength()
        {
            return data.Length;
        }

        public virtual String GetName()
        {
            return $"Item {id:X}";
        }

        public Int32 GetOriginalOffset()
        {
            return originalPosition;
        }

        public Int32 GetOriginalSize()
        {
            return originalSize;
        }

        public virtual ITwinItem GetRoot()
        {
            return root;
        }

        public virtual MemoryStream GetStream()
        {
            return stream;
        }

        public virtual void Read(BinaryReader reader, Int32 length)
        {
            data = reader.ReadBytes(length);
        }

        public virtual void SetID(UInt32 id)
        {
            this.id = id;
        }

        public virtual void SetIsLazy(Boolean isLazy)
        {
            this.isLazy = isLazy;
        }

        public void SetIsLoaded(Boolean isLoaded)
        {
            this.isLoaded = isLoaded;
        }

        public void SetOriginalOffset(Int32 offset)
        {
            originalPosition = offset;
        }

        public void SetOriginalSize(Int32 size)
        {
            originalSize = size;
        }

        public virtual void SetRoot(ITwinItem root)
        {
            this.root = root;
        }

        public virtual void SetStream(MemoryStream stream)
        {
            this.stream = stream;
        }

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write(data);
        }
    }
}
