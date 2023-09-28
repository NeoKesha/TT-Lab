using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class BehaviourCommandsSequenceData : AbstractAssetData
    {
        public BehaviourCommandsSequenceData()
        {
        }

        public BehaviourCommandsSequenceData(PS2AnyBehaviourCommandsSequence codeModel) : this()
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
            using FileStream fs = new(dataPath, System.IO.FileMode.Open, FileAccess.Read);
            using StreamReader reader = new(fs);
            var cm = new PS2AnyBehaviourCommandsSequence();
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
            PS2AnyBehaviourCommandsSequence codeModel = GetTwinItem<PS2AnyBehaviourCommandsSequence>();
            Code = codeModel.ToString();
            GenerateBehaviourGraphIdsList(codeModel);
        }

        private void GenerateBehaviourGraphIdsList(PS2AnyBehaviourCommandsSequence cm)
        {
            BehaviourGraphIds = new List<uint>();
            foreach (var e in cm.BehaviourPacks)
            {
                BehaviourGraphIds.Add(e.Key);
            }
        }
    }
}
