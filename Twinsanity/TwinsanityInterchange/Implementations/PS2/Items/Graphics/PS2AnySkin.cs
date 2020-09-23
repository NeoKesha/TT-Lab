using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnySkin : ITwinSkin
    {
        UInt32 id;
        public List<SubSkin> SubSkins { get; private set; }

        public PS2AnySkin()
        {
            SubSkins = new List<SubSkin>();
        }

        public UInt32 GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return 4 + SubSkins.Sum( (ss) => { return ss.GetLength(); });
        }

        public void Read(BinaryReader reader, int length)
        {
            var subSkins = reader.ReadInt32();
            for (int i = 0; i < subSkins; ++i)
            {
                var subSkin = new SubSkin();
                subSkin.Read(reader, length);
                SubSkins.Add(subSkin);
            }
        }

        public void SetID(UInt32 id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(SubSkins.Count);
            foreach (ITwinSerializable subSkin in SubSkins)
            {
                subSkin.Write(writer);
            }
        }
    }
}
