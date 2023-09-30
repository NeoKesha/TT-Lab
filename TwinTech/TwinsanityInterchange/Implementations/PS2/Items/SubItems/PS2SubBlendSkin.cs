using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems
{
    public class PS2SubBlendSkin : ITwinSubBlendSkin
    {
        public Int32 BlendsAmount { get; set; }
        public UInt32 Material { get; set; }
        public List<ITwinBlendSkinModel> Models { get; set; }

        public PS2SubBlendSkin()
        {
            Models = new List<ITwinBlendSkinModel>();
        }

        public int GetLength()
        {
            return 8 + Models.Sum((type1) => type1.GetLength());
        }

        public void Read(BinaryReader reader, int length)
        {
            var type1s = reader.ReadInt32();
            Material = reader.ReadUInt32();
            for (int i = 0; i < type1s; ++i)
            {
                var type1 = new PS2BlendSkinModel();
                type1.BlendsAmount = BlendsAmount;
                type1.Read(reader, length);
                Models.Add(type1);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Models.Count);
            writer.Write(Material);
            foreach (ITwinSerializable type1 in Models)
            {
                type1.Write(writer);
            }
        }
    }
}
