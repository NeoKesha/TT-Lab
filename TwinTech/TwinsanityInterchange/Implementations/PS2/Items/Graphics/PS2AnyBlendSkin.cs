using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnyBlendSkin : BaseTwinItem, ITwinBlendSkin
    {
        Int32 listLength;
        public List<SubBlendSkin> SubBlends;

        public PS2AnyBlendSkin()
        {
            SubBlends = new List<SubBlendSkin>();
        }

        public override Int32 GetLength()
        {
            return 8 + SubBlends.Sum((subBlend) => subBlend.GetLength());
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            var subBlends = reader.ReadInt32();
            listLength = reader.ReadInt32();
            for (int i = 0; i < subBlends; ++i)
            {
                var subBlend = new SubBlendSkin();
                subBlend.ListLength = listLength;
                subBlend.Read(reader, length);
                SubBlends.Add(subBlend);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(SubBlends.Count);
            writer.Write(listLength);
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
