using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout
{
    public class PS2AnyTrigger : ITwinTrigger
    {
        UInt32 id;
        public TwinTrigger Trigger { get; }
        public UInt16[] Argument { get; }
        public PS2AnyTrigger()
        {
            Argument = new UInt16[4];
            Trigger = new TwinTrigger();
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return Trigger.GetLength() + 8;
        }

        public void Read(BinaryReader reader, int length)
        {
            Trigger.Header1 = reader.ReadUInt32();
            Trigger.Enabled = reader.ReadUInt32();
            Trigger.HeaderT = reader.ReadSingle();
            Trigger.Rotation.Read(reader, Constants.SIZE_VECTOR4);
            Trigger.Position.Read(reader, Constants.SIZE_VECTOR4);
            Trigger.Scale.Read(reader, Constants.SIZE_VECTOR4);
            reader.ReadUInt32();
            UInt32 instances_cnt = reader.ReadUInt32();
            Trigger.HeaderH = reader.ReadUInt32();
            Trigger.Instances.Clear();
            for (int i = 0; i < instances_cnt; ++i)
            {
                Trigger.Instances.Add(reader.ReadUInt16());
            }
            for (int i = 0; i < 4; ++i)
            {
                Argument[i] = reader.ReadUInt16();
            }
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Trigger.Header1);
            writer.Write(Trigger.Enabled);
            writer.Write(Trigger.HeaderT);
            Trigger.Rotation.Write(writer);
            Trigger.Position.Write(writer);
            Trigger.Scale.Write(writer);
            writer.Write(Trigger.Instances.Count);
            writer.Write(Trigger.Instances.Count);
            writer.Write(Trigger.HeaderH);
            for (int i = 0; i < Trigger.Instances.Count; ++i)
            {
                writer.Write(Trigger.Instances[i]);
            }
            for (int i = 0; i < 4; ++i)
            {
                writer.Write(Argument[i]);
            }
        }

        public String GetName()
        {
            return $"Trigger {id:X}";
        }
    }
}
