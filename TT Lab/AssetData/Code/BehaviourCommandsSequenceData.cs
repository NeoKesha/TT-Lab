using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData.Code
{
    public class BehaviourCommandsSequenceData : AbstractAssetData
    {
        public BehaviourCommandsSequenceData()
        {
        }

        public BehaviourCommandsSequenceData(TwinBehaviourCommandsSequence codeModel) : this()
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
            var cm = new TwinBehaviourCommandsSequence();
            cm.ReadText(reader);
            Code = cm.ToString();
            GenerateBehaviourGraphIdsList(cm);
            SetTwinItem(cm);
        }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(LabURI package, String? variant)
        {
            TwinBehaviourCommandsSequence codeModel = GetTwinItem<TwinBehaviourCommandsSequence>();
            Code = codeModel.ToString();
            GenerateBehaviourGraphIdsList(codeModel);
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }

        private void GenerateBehaviourGraphIdsList(TwinBehaviourCommandsSequence cm)
        {
            BehaviourGraphIds = new List<uint>();
            foreach (var e in cm.BehaviourPacks)
            {
                BehaviourGraphIds.Add(e.Key);
            }
        }
    }
}
