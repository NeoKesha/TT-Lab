using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnyBlendSkin : BaseTwinItem, ITwinBlendSkin
    {
        Int32 blendsAmount;
        public List<ITwinSubBlendSkin> SubBlends { get; set; }

        public PS2AnyBlendSkin()
        {
            SubBlends = new List<ITwinSubBlendSkin>();
        }

        public override Int32 GetLength()
        {
            return 8 + SubBlends.Sum((subBlend) => subBlend.GetLength());
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            var subBlends = reader.ReadInt32();
            blendsAmount = reader.ReadInt32();
            for (int i = 0; i < subBlends; ++i)
            {
                var subBlend = new PS2SubBlendSkin();
                subBlend.BlendsAmount = blendsAmount;
                subBlend.Read(reader, length);
                SubBlends.Add(subBlend);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(SubBlends.Count);
            writer.Write(blendsAmount);
            foreach (ITwinSerializable subBlend in SubBlends)
            {
                subBlend.Write(writer);
            }
        }

        public override String GetName()
        {
            return $"Blend Skin {id:X}";
        }
    }
}
