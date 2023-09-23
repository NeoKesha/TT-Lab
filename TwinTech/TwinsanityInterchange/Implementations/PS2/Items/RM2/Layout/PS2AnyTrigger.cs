using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout
{
    public class PS2AnyTrigger : BaseTwinItem, ITwinTrigger
    {
        public TwinTrigger Trigger { get; }
        public UInt16[] Arguments { get; }
        public PS2AnyTrigger()
        {
            Arguments = new UInt16[4];
            Trigger = new TwinTrigger();
        }

        public override int GetLength()
        {
            return Trigger.GetLength() + 8;
        }

        public override void Read(BinaryReader reader, int length)
        {
            Trigger.Header = reader.ReadUInt32();
            Trigger.ObjectActivatorMask = reader.ReadUInt32();
            Trigger.UnkFloat = reader.ReadSingle();
            Trigger.Rotation.Read(reader, Constants.SIZE_VECTOR4);
            Trigger.Position.Read(reader, Constants.SIZE_VECTOR4);
            Trigger.Scale.Read(reader, Constants.SIZE_VECTOR4);
            reader.ReadUInt32();
            UInt32 instances_cnt = reader.ReadUInt32();
            Trigger.InstanceExtensionValue = reader.ReadUInt32();
            Trigger.Instances.Clear();
            for (int i = 0; i < instances_cnt; ++i)
            {
                Trigger.Instances.Add(reader.ReadUInt16());
            }
            for (int i = 0; i < 4; ++i)
            {
                Arguments[i] = reader.ReadUInt16();
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Trigger.Header);
            writer.Write(Trigger.ObjectActivatorMask);
            writer.Write(Trigger.UnkFloat);
            Trigger.Rotation.Write(writer);
            Trigger.Position.Write(writer);
            Trigger.Scale.Write(writer);
            writer.Write(Trigger.Instances.Count);
            writer.Write(Trigger.Instances.Count);
            writer.Write(Trigger.InstanceExtensionValue);
            for (int i = 0; i < Trigger.Instances.Count; ++i)
            {
                writer.Write(Trigger.Instances[i]);
            }
            for (int i = 0; i < 4; ++i)
            {
                writer.Write(Arguments[i]);
            }
        }

        public override String GetName()
        {
            return $"Trigger {id:X}";
        }
    }
}
