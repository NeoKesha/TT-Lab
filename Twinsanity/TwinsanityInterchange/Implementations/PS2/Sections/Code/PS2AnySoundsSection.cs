using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.Code
{
    public class PS2AnySoundsSection : BaseTwinSection
    {
        public PS2AnySoundsSection() : base()
        {
            defaultType = typeof(PS2AnySound);
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            base.Read(reader, length);
            var offset = 0;
            foreach (PS2AnySound item in Items)
            {
                Array.Copy(extraData, offset, item.Sound, 0, item.Sound.Length);
                offset += item.Sound.Length;
            }
        }

        public override void rebuildExtraData()
        {
            using (var newExtraData = new MemoryStream())
            {
                var offset = 0;
                foreach (PS2AnySound item in Items)
                {
                    newExtraData.Write(item.Sound, 0, item.Sound.Length);
                    item.offset = offset;
                    offset += item.Sound.Length;
                }
                extraData = newExtraData.ToArray();
            }
        }
    }
}
