using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class SubBlendSkin : ITwinSerializable
    {
        public Int32 ListLength { get; set; }
        public UInt32 Material;
        public List<BlendSkinType1> Type1s;

        public SubBlendSkin()
        {
            Type1s = new List<BlendSkinType1>();
        }

        public int GetLength()
        {
            return 8 + Type1s.Sum((type1) => type1.GetLength());
        }

        public void Read(BinaryReader reader, int length)
        {
            var type1s = reader.ReadInt32();
            Material = reader.ReadUInt32();
            for (int i = 0; i < type1s; ++i)
            {
                var type1 = new BlendSkinType1();
                type1.ListLength = ListLength;
                type1.Read(reader, length);
                Type1s.Add(type1);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Type1s.Count);
            writer.Write(Material);
            foreach (ITwinSerializable type1 in Type1s)
            {
                type1.Write(writer);
            }
        }
    }
}
