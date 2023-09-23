using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code
{
    public abstract class PS2BehaviourWrapper : BaseTwinItem, ITwinBehaviour
    {
        private UInt16 scriptID;
        public Byte Mask;

        public override int GetLength()
        {
            return 4;
        }

        public override void Read(BinaryReader reader, int length)
        {
            scriptID = reader.ReadUInt16();
            Mask = reader.ReadByte();
            reader.ReadByte(); // Skip flag
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(scriptID);
            writer.Write(Mask);
            writer.Write((Byte)((id + 1) % 2));
        }
    }
}
