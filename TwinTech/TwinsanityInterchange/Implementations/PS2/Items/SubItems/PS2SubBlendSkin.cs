﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems
{
    public class PS2SubBlendSkin : ITwinSubBlendSkin
    {
        public Int32 BlendsAmount { get; set; }
        public UInt32 Material { get; set; }
        public List<ITwinBlendSkinModel> Models { get; set; }

        public PS2SubBlendSkin()
        {
            Models = new List<ITwinBlendSkinModel>();
        }

        public int GetLength()
        {
            return 8 + Models.Sum((model) => model.GetLength());
        }

        public void Read(BinaryReader reader, int length)
        {
            var modelsAmount = reader.ReadInt32();
            Material = reader.ReadUInt32();
            for (int i = 0; i < modelsAmount; ++i)
            {
                var model = new PS2BlendSkinModel();
                model.BlendsAmount = BlendsAmount;
                model.Read(reader, length);
                Models.Add(model);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Models.Count);
            writer.Write(Material);
            foreach (ITwinSerializable model in Models)
            {
                model.Write(writer);
            }
        }
    }
}
