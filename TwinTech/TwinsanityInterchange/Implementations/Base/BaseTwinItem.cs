using System;
using System.IO;
using Twinsanity.Libraries;
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
        protected String hash;
        Byte[] data;
        public BaseTwinItem()
        {
            data = Array.Empty<Byte>();
        }
        public BaseTwinItem(Byte[] data)
        {
            this.data = data;
        }

        public void ComputeHash(Stream stream)
        {
            hash = Hasher.ComputeHash(stream);
        }

        public void ComputeHash(Stream stream, UInt32 length)
        {
            hash = Hasher.ComputeHash(stream, length);
        }

        public virtual String GetHash()
        {
            return hash;
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
