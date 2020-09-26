using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Code;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.Code
{
    public class PS2AnyScriptsSection : BaseTwinSection
    {
        public PS2AnyScriptsSection() : base()
        {
            defaultType = typeof(PS2AnyScript);
            idToClassDictionary[0] = typeof(PS2HeaderScript);
            idToClassDictionary[1] = typeof(PS2MainScript);
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            if (length > 0)
            {
                Int64 baseOffset = reader.BaseStream.Position;
                magicNumber = reader.ReadUInt32();
                UInt32 itemsCount = reader.ReadUInt32();
                UInt32 streamLength = reader.ReadUInt32();
                Record[] records = new Record[itemsCount];
                for (int i = 0; i < itemsCount; ++i)
                {
                    Record record = new Record();
                    record.Read(reader, 12);
                    records[i] = record;
                }
                Items.Clear();
                for (int i = 0; i < itemsCount; ++i)
                {
                    ITwinItem item = null;
                    // The only change needed to base item reading, not sure how to do this without having to copypaste
                    if (idToClassDictionary.ContainsKey(records[i].ItemId % 2))
                    {
                        Type type = idToClassDictionary[records[i].ItemId % 2];
                        item = (ITwinItem)Activator.CreateInstance(type);
                    }
                    reader.BaseStream.Position = records[i].Offset + baseOffset;
                    item.Read(reader, (Int32)records[i].Size);
                    item.SetID(records[i].ItemId);
                    Items.Add(item);
                }
                extraData = reader.ReadBytes((Int32)(length - (reader.BaseStream.Position - baseOffset)));
            }
            else
            {
                skip = true;
            }
        }
    }
}
