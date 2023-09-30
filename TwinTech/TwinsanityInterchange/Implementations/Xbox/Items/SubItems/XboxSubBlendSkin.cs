using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Implementations.Xbox.Items.SubItems
{
    public class XboxSubBlendSkin : ITwinSubBlendSkin
    {
        public Int32 BlendsAmount { get; set; }
        public UInt32 Material { get; set; }
        public List<ITwinBlendSkinModel> Models { get; set; }

        public XboxSubBlendSkin()
        {
            Models = new();
        }


        public int GetLength()
        {
            return 4 + Models.Sum((model) => model.GetLength());
        }

        public void Read(BinaryReader reader, int length)
        {
            Material = reader.ReadUInt32();
            var model = new XboxBlendSkinModel();
            model.BlendsAmount = BlendsAmount;
            model.Read(reader, length);
            Models.Add(model);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Material);
            foreach (ITwinSerializable model in Models)
            {
                model.Write(writer);
            }
        }
    }
}
