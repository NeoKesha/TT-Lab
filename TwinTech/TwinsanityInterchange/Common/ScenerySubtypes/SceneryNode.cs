using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes
{
    public class SceneryNode : SceneryBaseType
    {
        public Int32[] SceneryTypes;

        public SceneryNode()
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
            Read(reader, length);
            foreach (var sceneryType in SceneryTypes)
            {
                // This is pretty tough since these are the only 2 types besides the Root type
                // of course a Dictionary Int -> Type could work but I decided to opt for simple
                // if checks since I don't see it worth the effort to make a mess out of Read
                // method calls.
                //                                                                      ~Smartkin
                if (sceneryType == 0x1600)
                {
                    var newSc = new SceneryNode();
                    sceneries.Add(newSc);
                    newSc.Read(reader, length, sceneries);
                }
                if (sceneryType == 0x1605)
                {
                    var newSc = new SceneryLeaf();
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

        public override Int32 GetObjectIndex()
        {
            return 0x1600;
        }
    }
}
