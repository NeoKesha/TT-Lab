using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnyModel : BaseTwinItem, ITwinModel
    {
        public List<SubModel> SubModels { get; set; }
        public PS2AnyModel()
        {
            SubModels = new List<SubModel>();
        }

        public override Int32 GetLength()
        {
            Int32 totalLength = 4;
            foreach (ITwinSerializable e in SubModels)
            {
                totalLength += e.GetLength();
            }
            return totalLength;
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            int subCnt = reader.ReadInt32();
            SubModels.Clear();
            for (int i = 0; i < subCnt; ++i)
            {
                SubModel model = new SubModel();
                model.Read(reader, length);
                SubModels.Add(model);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(SubModels.Count);
            foreach (ITwinSerializable e in SubModels)
            {
                e.Write(writer);
            }
        }

        public override String GetName()
        {
            return $"Model {id:X}";
        }
    }
}
