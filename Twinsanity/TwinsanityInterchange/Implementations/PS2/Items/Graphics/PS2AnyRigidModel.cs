using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnyRigidModel : ITwinRigidModel
    {
        UInt32 id;
        public UInt32 Header { get; set; } // Unused by the game
        public List<UInt32> Materials { get; private set; }
        public UInt32 Model { get; set; }

        public PS2AnyRigidModel()
        {
            Materials = new List<UInt32>();
        }

        public UInt32 GetID()
        {
            return id;
        }

        public Int32 GetLength()
        {
            return 8 + Materials.Count * Constants.SIZE_UINT32;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            Header = reader.ReadUInt32();
            var matAmt = reader.ReadInt32();
            for (int i = 0; i < matAmt; ++i)
            {
                Materials.Add(reader.ReadUInt32());
            }
            Model = reader.ReadUInt32();
        }

        public void SetID(UInt32 id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Header);
            for (int i = 0; i < Materials.Count; ++i)
            {
                writer.Write(Materials[i]);
            }
            writer.Write(Model);
        }
    }
}
