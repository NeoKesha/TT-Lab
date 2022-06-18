using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX.Items.Graphics
{
    public class XBOXAnyBlendSkin : PS2AnyBlendSkin
    {
        public UInt32 BlendShapeCount { get; set; }

        public override Int32 GetLength()
        {
            Int32 totalLength = 8;
            foreach (ITwinSerializable e in SubBlends)
            {
                totalLength += e.GetLength();
            }
            return totalLength;
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            int subCnt = reader.ReadInt32();
            BlendShapeCount = reader.ReadUInt32();
            SubBlends.Clear();
            for (int i = 0; i < subCnt; ++i)
            {
                SubBlendSkinXBOX model = new SubBlendSkinXBOX(BlendShapeCount);
                model.Read(reader, length);
                SubBlends.Add(model);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(SubBlends.Count);
            writer.Write(BlendShapeCount);
            foreach (ITwinSerializable e in SubBlends)
            {
                e.Write(writer);
            }
        }
    }
}
