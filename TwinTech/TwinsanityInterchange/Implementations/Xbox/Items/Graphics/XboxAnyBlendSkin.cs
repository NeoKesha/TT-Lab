using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.Xbox.Items.SubItems;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Implementations.Xbox.Items.Graphics
{
    public class XboxAnyBlendSkin : BaseTwinItem, ITwinBlendSkin
    {
        Int32 blendsAmount;
        public List<ITwinSubBlendSkin> SubBlends { get; set; }

        public XboxAnyBlendSkin()
        {
            SubBlends = new();
        }

        public override Int32 GetLength()
        {
            return 8 + SubBlends.Sum((subBlend) => subBlend.GetLength());
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            var subBlends = reader.ReadInt32();
            blendsAmount = reader.ReadInt32();
            for (Int32 i = 0; i < subBlends; i++)
            {
                var subBlend = new XboxSubBlendSkin();
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
