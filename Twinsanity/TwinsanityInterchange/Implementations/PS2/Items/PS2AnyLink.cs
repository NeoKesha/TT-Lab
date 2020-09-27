using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items
{
    public class PS2AnyLink : ITwinLink
    {
        UInt32 id;
        public List<TwinChunkLink> LinksList;
        
        public PS2AnyLink()
        {
            LinksList = new List<TwinChunkLink>();
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            int linksSize = 0;
            foreach (ITwinSerializable e in LinksList)
            {
                linksSize += e.GetLength();
            }
            return 4 + linksSize;
        }

        public void Read(BinaryReader reader, int length)
        {
            int links = reader.ReadInt32();
            LinksList.Clear();
            for (int i = 0; i < links; ++i)
            {
                TwinChunkLink link = new TwinChunkLink();
                link.Read(reader, length);
                LinksList.Add(link);
            }
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(LinksList.Count);
            foreach (ITwinSerializable e in LinksList)
            {
                e.Write(writer);
            }
        }
    }
}
