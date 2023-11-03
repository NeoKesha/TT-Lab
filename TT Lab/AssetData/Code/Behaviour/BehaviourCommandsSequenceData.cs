using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.Xbox.Items.RMX.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab;

namespace TT_Lab.AssetData.Code.Behaviour
{
    public class BehaviourCommandsSequenceData : AbstractAssetData
    {
        public BehaviourCommandsSequenceData()
        {
        }

        public BehaviourCommandsSequenceData(ITwinBehaviourCommandsSequence codeModel) : this()
        {
            SetTwinItem(codeModel);
        }

        public String Code { get; set; }
        public List<UInt32> BehaviourGraphIds { get; set; }

        public override void Save(string dataPath, JsonSerializerSettings? settings = null)
        {
            using FileStream fs = new(dataPath, FileMode.Create, FileAccess.Write);
            using BinaryWriter writer = new(fs);
            writer.Write(Code.ToCharArray());
        }

        public override void Load(String dataPath, JsonSerializerSettings? settings = null)
        {
            using FileStream fs = new(dataPath, FileMode.Open, FileAccess.Read);
            using StreamReader reader = new(fs);
            var line = reader.ReadLine()!.Trim();
            ITwinBehaviourCommandsSequence cm;

            if (line == "@Xbox sequence")
            {
                cm = new XboxBehaviourCommandsSequence();
            }
            else
            {
                cm = new PS2BehaviourCommandsSequence();
            }

            reader.BaseStream.Position = 0;
            cm.ReadText(reader);
            Code = cm.ToString();
            GenerateBehaviourGraphIdsList(cm);
            SetTwinItem(cm);
        }

        protected override void Dispose(Boolean disposing)
        {
            BehaviourGraphIds.Clear();
            Code = "";
        }

        public override void Import(LabURI package, String? variant)
        {
            var codeModel = GetTwinItem<ITwinBehaviourCommandsSequence>();
            Code = codeModel.ToString();
            GenerateBehaviourGraphIdsList(codeModel);
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            writer.Write(Code);

            ms.Position = 0;
            return factory.GenerateBehaviourCommandsSequence(ms);
        }

        private void GenerateBehaviourGraphIdsList(ITwinBehaviourCommandsSequence cm)
        {
            BehaviourGraphIds = new List<UInt32>();
            foreach (var e in cm.BehaviourPacks)
            {
                BehaviourGraphIds.Add(e.Key);
            }
        }
    }
}
