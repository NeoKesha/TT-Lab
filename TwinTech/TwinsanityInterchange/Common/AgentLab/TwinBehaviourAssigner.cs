using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class TwinBehaviourAssigner : ITwinSerializable
    {
        public Int32 Behaviour { get; set; }
        public UInt16 Object { get; set; }
        public AssignTypeID AssignType { get; set; }
        public AssignLocalityID AssignLocality { get; set; }
        public AssignStatusID AssignStatus { get; set; }
        public AssignPreferenceID AssignPreference { get; set; }

        public Int32 GetLength()
        {
            return 8;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            Behaviour = reader.ReadInt32();
            var assigner = reader.ReadUInt32();
            {
                Object = (UInt16)(assigner >> 0x10);
                AssignType = (AssignTypeID)(assigner & 0xF);
                AssignLocality = (AssignLocalityID)(assigner >> 0x4 & 0xF);
                AssignStatus = (AssignStatusID)(assigner >> 0x8 & 0xF);
                AssignPreference = (AssignPreferenceID)(assigner >> 0xC & 0xF);
            }

        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Behaviour);
            UInt32 newAssigner = (UInt32)(Object & 0xFFFF << 0x10);
            newAssigner |= (UInt32)AssignType;
            newAssigner |= (UInt32)AssignLocality << 0x4;
            newAssigner |= (UInt32)AssignStatus << 0x8;
            newAssigner |= (UInt32)AssignPreference << 0xC;
            writer.Write(newAssigner);
        }

        public enum AssignTypeID
        {
            ME = 0,
            OBJECT_CHILD,
            LINKED_OBJECT,
            GLOBAL_AGENT,
            HUMAN_PLAYER,
            BACKGROUND_CHARACTER,
            ANYBODY,
            GENERATE_AGENT,
            ORIGINATOR,
        }
        public enum AssignLocalityID
        {
            NEARBY = 0,
            LOCAL,
            GLOBAL,
            ANYWHERE,
        }
        public enum AssignStatusID
        {
            IDLE = 0,
            BUSY,
            ANYSTATE,
        }
        public enum AssignPreferenceID
        {
            NEAREST = 0,
            FURTHEST,
            STRONGEST,
            WEAKEST,
            BEST_ALIGNED,
            ANYHOW,
        }
    }
}
