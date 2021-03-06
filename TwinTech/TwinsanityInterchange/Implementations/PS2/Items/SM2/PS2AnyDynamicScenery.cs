﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2
{
    public class PS2AnyDynamicScenery : BaseTwinItem, ITwinDynamicScenery
    {

        public Int32 UnkInt;
        public List<DynamicSceneryModel> DynamicModels;

        public PS2AnyDynamicScenery()
        {
            DynamicModels = new List<DynamicSceneryModel>();
        }

        public override Int32 GetLength()
        {
            return 6 + DynamicModels.Sum(d => d.GetLength());
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            UnkInt = reader.ReadInt32();
            var models = reader.ReadInt16();
            for (var i = 0; i < models; ++i)
            {
                var m = new DynamicSceneryModel();
                DynamicModels.Add(m);
                m.Read(reader, length);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(UnkInt);
            writer.Write((Int16)DynamicModels.Count);
            foreach (var model in DynamicModels)
            {
                model.Write(writer);
            }
        }

        public override String GetName()
        {
            return $"Dynamic scenery {id:X}";
        }
    }
}
