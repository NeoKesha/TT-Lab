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
    public class XBOXAnyModel : PS2AnyModel
    {
        public override void Read(BinaryReader reader, Int32 length)
        {
            int subCnt = reader.ReadInt32();
            SubModels.Clear();
            for (int i = 0; i < subCnt; ++i)
            {
                SubModelXBOX model = new SubModelXBOX();
                model.Read(reader, length);
                SubModels.Add(model);
            }
        }
    }
}
