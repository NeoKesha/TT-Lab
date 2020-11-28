using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes
{
    public class SceneryType_0x1600 : SceneryBaseType
    {
        public Int32[] SceneryTypes;

        public SceneryType_0x1600()
        {
            SceneryTypes = new Int32[8];
        }

        public override Int32 GetLength()
        {
            return base.GetLength() + SceneryTypes.Length * 4;
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            base.Read(reader, length);
            for (var i = 0; i < SceneryTypes.Length; ++i)
            {
                SceneryTypes[i] = reader.ReadInt32();
            }
        }

        public virtual void Read(BinaryReader reader, Int32 length, IList<SceneryBaseType> sceneries)
        {
            foreach (var sceneryType in SceneryTypes)
            {
                // This is pretty tough since these are the only 2 types besides the Root type
                // of course a Dictionary Int -> Type could work but I decided to opt for simple
                // if checks since I don't see it worth the effort to make a mess out of Read
                // method calls.
                //                                                                      ~Smartkin
                if (sceneryType == 0x1600)
                {
                    var newSc = new SceneryType_0x1600();
                    sceneries.Add(newSc);
                    newSc.Read(reader, length, sceneries);
                }
                if (sceneryType == 0x1605)
                {
                    var newSc = new SceneryType_0x1605();
                    sceneries.Add(newSc);
                    newSc.Read(reader, length);
                }
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            foreach (var type in SceneryTypes)
            {
                writer.Write(type);
            }
        }
    }
}
