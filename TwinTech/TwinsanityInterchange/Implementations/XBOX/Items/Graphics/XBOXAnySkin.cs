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
    public class XBOXAnySkin : PS2AnySkin
    {
        public override Int32 GetLength()
        {
            Int32 totalLength = 4;
            foreach (ITwinSerializable e in SubSkins)
            {
                totalLength += e.GetLength();
            }
            return totalLength;
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            int subCnt = reader.ReadInt32();
            SubSkins.Clear();
            for (int i = 0; i < subCnt; ++i)
            {
                SubSkinXBOX model = new SubSkinXBOX();
                model.Read(reader, length);
                SubSkins.Add(model);
            }
        }
    }
}
