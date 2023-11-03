using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class TwinBehaviourStarter : TwinBehaviourWrapper
    {
        public List<TwinBehaviourAssigner> Assigners;

        public TwinBehaviourStarter()
        {
            Assigners = new List<TwinBehaviourAssigner>();
        }

        public override int GetLength()
        {
            return base.GetLength() + 4 + Assigners.Count * (Constants.SIZE_UINT32 + Constants.SIZE_UINT32);
        }

        public override String GetName()
        {
            return $"Behaviour Starter {id:X}";
        }

        public override void Read(BinaryReader reader, int length)
        {
            base.Read(reader, length);
            var amount = reader.ReadUInt32();
            for (var i = 0; i < amount; ++i)
            {
                var assigner = new TwinBehaviourAssigner();
                assigner.Read(reader, length);
                Assigners.Add(assigner);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(Assigners.Count);
            foreach (var assigner in Assigners)
            {
                assigner.Write(writer);
            }
        }
    }
}
